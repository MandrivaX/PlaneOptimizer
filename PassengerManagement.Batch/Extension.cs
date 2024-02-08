using PassengerManagement.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace PassengerManagement.Batch
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The available places
        /// </summary>
        public const int AvailablePlace = 200;

        /// <summary>
        /// The optimized turnover message
        /// </summary>
        public static readonly string OptimizedTurnoverMessage = "The Optimized turnover is :{0}";

        /// <summary>
        /// Convert args to passengers
        /// </summary>
        /// <param name="args">The args</param>
        /// <returns>The list of passenger</returns>
        public static IList<Passenger> GetPassengers()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Passengers\\Passengers.txt");

            if (!File.Exists(path))
            {
                path = Path.Combine("/Passengers/Passengers.txt");
            }

            string[] lines = File.ReadAllLines(path);
            List<Passenger> passengers = new();
            bool isFirstLine = true;
            foreach (string line in lines)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                string[] parts = line.Split(',');

                if (parts.Length >= 5)
                {
                    int id = int.Parse(parts[0]);
                    int age = int.Parse(parts[2]);
                    string family = parts[3] == "-" ? Guid.NewGuid().ToString() : parts[3];
                    bool needTwoPlaces = parts[4].Equals("Oui", StringComparison.OrdinalIgnoreCase);

                    Passenger passenger = new Passenger
                    {
                        Id = id,
                        Age = age,
                        NeedTwoPlaces = needTwoPlaces,
                        FamilyName = family
                    };

                    passengers.Add(passenger);
                }
            }
            return passengers;
        }
    }
}
