using PassengerManagement.Constants;

namespace PassengerManagement.Entities
{
    /// <summary>
    /// The passenger
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// id for passenger
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The age of passenger
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// True, if passenger need two place, false if else
        /// </summary>
        public bool NeedTwoPlaces { get; set; }

        /// <summary>
        /// The family name
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// Type of passenger (Adult or Children)
        /// </summary>
        public PassengerType Type => Age > 12 ? PassengerType.Adult : PassengerType.Children;

        /// <summary>
        /// Price 
        /// </summary>
        public decimal Price => Type == PassengerType.Adult ? NeedTwoPlaces ?
            PassengerManagementConstants.TwoPlaces * PassengerManagementConstants.PriceAdulte
            : PassengerManagementConstants.PriceAdulte 
            : PassengerManagementConstants.PriceChildren;
    }
}
