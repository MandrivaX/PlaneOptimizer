using PassengerManagement.Entities;
using System.Collections.Generic;

namespace PassengerManagement.Services
{
    /// <summary>
    /// Interface for Passenger Management service
    /// </summary>
    public interface IPassengerManagementService
    {
        /// <summary>
        /// Check rules constraints and Convert passengers to families 
        /// </summary>
        /// <param name="passengers">The list of passengers</param>
        /// <returns>The list of family</returns>
        public List<Family> CheckRulesAndGetFamilies(IList<Passenger> passengers);

        /// <summary>
        /// Get the optimized turnover
        /// </summary>
        /// <param name="families">The list of families</param>
        /// <param name="availablePlace">The available place</param>
        /// <returns>The turnover optimized</returns>
        public decimal GetOptimizedTurnover(List<Family> families, int availablePlace);
    }
}
