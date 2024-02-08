using PassengerManagement.Constants;
using System.Collections.Generic;
using System.Linq;

namespace PassengerManagement.Entities
{
    /// <summary>
    /// Family
    /// </summary>
    public class Family
    {
        /// <summary>
        /// Name of family
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Members of family
        /// </summary>
        public IList<Passenger> Members { get; set; }

        /// <summary>
        /// Total price for family
        /// </summary>
        public decimal TotalPrice => Members.Sum(m => m.Price);

        /// <summary>
        /// Total place for family
        /// </summary>
        public int TotalPlace => PassengerManagementConstants.TwoPlaces * Members.Count(m => m.NeedTwoPlaces) 
            + Members.Count(m => !m.NeedTwoPlaces);
    }
}
