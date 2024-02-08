using Microsoft.Extensions.Logging;
using PassengerManagement.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PassengerManagement.Services
{
    public class PassengerManagementService : IPassengerManagementService
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger<IPassengerManagementService> _logger;

        /// <summary>
        /// Passenger management service ctor
        /// </summary>
        /// <param name="logger"></param>
        public PassengerManagementService(ILogger<IPassengerManagementService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Check rules constraint and convert passenger to families
        /// </summary>
        /// <param name="passengers">The list of passengers</param>
        /// <returns>The list of family</returns>
        public List<Family> CheckRulesAndGetFamilies(IList<Passenger> passengers)
        {
            if (passengers == null || !passengers.Any())
            {
                return new List<Family>();
            }

            IEnumerable<Family> families = passengers.GroupBy(_ => _.FamilyName).Select(p => new Family
            {
                Members = p.ToList(),
                Name = p.Key
            });

            return families.Where(f => f.Members.Any(m => m.Type == PassengerType.Adult)
                && f.Members.Count(m => m.Type == PassengerType.Adult) <= 2
                && f.Members.Count(m => m.Type == PassengerType.Children) <= 3
                && !f.Members.Any(m => m.Type == PassengerType.Children && m.NeedTwoPlaces)).ToList();
        }

        /// <summary>
        /// Get and Calculate Turnover
        /// </summary>
        /// <param name="families">The list of family</param>
        /// <param name="availablePlace">The available places</param>
        /// <returns>The turnover</returns>
        public decimal GetOptimizedTurnover(List<Family> families, int availablePlace)
        {
            _logger.LogInformation("Start to calculate optimized turnover");

            if(families == null || !families.Any() || availablePlace == 0)
            {
                return 0;
            }

            families = families.OrderBy(family => family.TotalPlace)
                    .ThenByDescending(family => family.TotalPrice)
                    .ToList();

            List<Family> selectedFamilies = new();

            while (families.Any() && availablePlace > 0)
            {
                var profitableFamily = families.FirstOrDefault(family => family.TotalPlace <= availablePlace);

                if (profitableFamily != null)
                {
                    selectedFamilies.Add(profitableFamily);
                    availablePlace -= profitableFamily.TotalPlace;
                    families.Remove(profitableFamily);
                }
                else
                {
                    if (!CheckAndUpdateSelectedFamilies(families, ref availablePlace, selectedFamilies))
                    {
                        break;
                    }
                }
            }

            _logger.LogInformation("End calculate turnover");

            return selectedFamilies.Sum(f => f.TotalPrice);
        }

        /// <summary>
        /// Check if it's possible to replace the selected family with a family that is not selected but has a higher total price.
        /// </summary>
        /// <param name="families">The families</param>
        /// <param name="availablePlace">available places</param>
        /// <param name="selectedFamilies">selected families</param>
        private bool CheckAndUpdateSelectedFamilies(List<Family> families, ref int availablePlace, List<Family> selectedFamilies)
        {
            bool isSelectedFamiliesUpdated = false;

            foreach (var family in new List<Family>(families))
            {
                var selectedFamilyToRemove = selectedFamilies.Where(f => f.TotalPrice < family.TotalPrice).OrderBy(f => f.TotalPrice);
                decimal sumTotalPlace = 0;
                int availablePlaceAdded = availablePlace;
                List<Family> familiesToRemove = new();

                foreach (var familySelected in selectedFamilyToRemove)
                {
                    if (sumTotalPlace <= family.TotalPrice)
                    {
                        sumTotalPlace += familySelected.TotalPrice;
                        availablePlaceAdded += familySelected.TotalPlace;
                        familiesToRemove.Add(familySelected);
                    }
                }

                if (families.FirstOrDefault(family => family.TotalPlace < availablePlaceAdded) != null)
                {
                    families.InsertRange(1, familiesToRemove);
                    selectedFamilies.RemoveAll(f => familiesToRemove.Any(fr => fr.Name == f.Name));
                    availablePlace = availablePlaceAdded;
                    isSelectedFamiliesUpdated = true;
                }
            }

            return isSelectedFamiliesUpdated;
        }
    }
}
