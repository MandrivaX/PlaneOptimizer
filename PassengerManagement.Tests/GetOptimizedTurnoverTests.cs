using Microsoft.Extensions.Logging;
using Moq;
using PassengerManagement.Entities;
using PassengerManagement.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PassengerManagement.Tests
{
    public class GetOptimizedTurnoverTests
    {
        [Fact]
        public void GetOptimizedTurnover_WithoutCheckSelectedFamilies_Success()
        {
            var logger = new Mock<ILogger<IPassengerManagementService>>().Object;
            var service = new PassengerManagementService(logger);

            //Arrange
            var passengers = GetPassengers();

            var families = passengers.GroupBy(p => p.FamilyName)
                                     .Select(group => new Family
                                     {
                                         Name = group.Key,
                                         Members = group.ToList()
                                     })
                                     .ToList();
            int availablePlace = 20;

            //Act
            var result = service.GetOptimizedTurnover(families, availablePlace);

            //Assert
            decimal expected = 4500;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetOptimizedTurnover_WithCheckSelectedFamilies_Success()
        {
            var logger = new Mock<ILogger<IPassengerManagementService>>().Object;
            var service = new PassengerManagementService(logger);

            // Arrange
            var passengers = new List<Passenger> {
                new Passenger { Id = 1, Age = 35, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 2, Age = 32, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 3, Age = 7, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 5, Age = 45, NeedTwoPlaces = false, FamilyName = "B" },
                new Passenger { Id = 6, Age = 10, NeedTwoPlaces = false, FamilyName = "B" },
            };

            var families = passengers.GroupBy(p => p.FamilyName)
                                     .Select(group => new Family
                                     {
                                         Name = group.Key,
                                         Members = group.ToList()
                                     })
                                     .ToList();
            int availablePlace = 4;

            // Act
            var result = service.GetOptimizedTurnover(families, availablePlace);

            // Assert
            decimal expected = 650;
            Assert.Equal(expected, result);
        }

        private List<Passenger> GetPassengers()
        {
            return new List<Passenger>
            {
                new Passenger { Id = 1, Age = 35, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 2, Age = 32, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 3, Age = 7, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 4, Age = 4, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 5, Age = 45, NeedTwoPlaces = false, FamilyName = "B" },
                new Passenger { Id = 6, Age = 10, NeedTwoPlaces = false, FamilyName = "B" },
                new Passenger { Id = 7, Age = 5, NeedTwoPlaces = false, FamilyName = "B" },
                new Passenger { Id = 8, Age = 28, NeedTwoPlaces = true, FamilyName = "C" },
                new Passenger { Id = 9, Age = 27, NeedTwoPlaces = false, FamilyName = "C" },
                new Passenger { Id = 10, Age = 2, NeedTwoPlaces = false, FamilyName = "C" },
                new Passenger { Id = 11, Age = 40, NeedTwoPlaces = false, FamilyName = "D" },
                new Passenger { Id = 12, Age = 11, NeedTwoPlaces = false, FamilyName = "D" },
                new Passenger { Id = 13, Age = 55, NeedTwoPlaces = false, FamilyName = "A1" },
                new Passenger { Id = 14, Age = 29, NeedTwoPlaces = true, FamilyName = "A2" },
                new Passenger { Id = 15, Age = 22, NeedTwoPlaces = false, FamilyName = "A3" },
                new Passenger { Id = 16, Age = 33, NeedTwoPlaces = false, FamilyName = "H" },
                new Passenger { Id = 17, Age = 31, NeedTwoPlaces = false, FamilyName = "H" },
                new Passenger { Id = 18, Age = 9, NeedTwoPlaces = false, FamilyName = "H" },
                new Passenger { Id = 19, Age = 8, NeedTwoPlaces = false, FamilyName = "H" },
                new Passenger { Id = 20, Age = 6, NeedTwoPlaces = false, FamilyName = "H" },
                new Passenger { Id = 21, Age = 50, NeedTwoPlaces = false, FamilyName = "I" },
                new Passenger { Id = 22, Age = 12, NeedTwoPlaces = false, FamilyName = "I" },
                new Passenger { Id = 23, Age = 38, NeedTwoPlaces = false, FamilyName = "A4" },
                new Passenger { Id = 24, Age = 41, NeedTwoPlaces = false, FamilyName = "J" },
                new Passenger { Id = 25, Age = 3, NeedTwoPlaces = false, FamilyName = "J" },
                new Passenger { Id = 26, Age = 10, NeedTwoPlaces = false, FamilyName = "J" },
                new Passenger { Id = 27, Age = 48, NeedTwoPlaces = false, FamilyName = "K" },
                new Passenger { Id = 28, Age = 7, NeedTwoPlaces = false, FamilyName = "K" },
                new Passenger { Id = 29, Age = 11, NeedTwoPlaces = false, FamilyName = "K" },
                new Passenger { Id = 30, Age = 27, NeedTwoPlaces = false, FamilyName = "L" },
                new Passenger { Id = 31, Age = 25, NeedTwoPlaces = false, FamilyName = "L" },
                new Passenger { Id = 32, Age = 4, NeedTwoPlaces = false, FamilyName = "L" },
                new Passenger { Id = 33, Age = 47, NeedTwoPlaces = false, FamilyName = "M" },
                new Passenger { Id = 34, Age = 42, NeedTwoPlaces = false, FamilyName = "M" },
                new Passenger { Id = 35, Age = 6, NeedTwoPlaces = false, FamilyName = "M" },
                new Passenger { Id = 36, Age = 8, NeedTwoPlaces = false, FamilyName = "M" },
                new Passenger { Id = 37, Age = 36, NeedTwoPlaces = false, FamilyName = "N" },
                new Passenger { Id = 38, Age = 34, NeedTwoPlaces = false, FamilyName = "N" },
                new Passenger { Id = 39, Age = 9, NeedTwoPlaces = false, FamilyName = "N" },
                new Passenger { Id = 40, Age = 12, NeedTwoPlaces = false, FamilyName = "N" },
                new Passenger { Id = 41, Age = 53, NeedTwoPlaces = false, FamilyName = "A5" },
                new Passenger { Id = 42, Age = 26, NeedTwoPlaces = true, FamilyName = "A6" },
                new Passenger { Id = 43, Age = 31, NeedTwoPlaces = false, FamilyName = "A7" },
                new Passenger { Id = 44, Age = 28, NeedTwoPlaces = false, FamilyName = "O" },
                new Passenger { Id = 45, Age = 30, NeedTwoPlaces = false, FamilyName = "O" },
                new Passenger { Id = 46, Age = 7, NeedTwoPlaces = false, FamilyName = "O" },
                new Passenger { Id = 47, Age = 5, NeedTwoPlaces = false, FamilyName = "O" },
                new Passenger { Id = 48, Age = 37, NeedTwoPlaces = false, FamilyName = "P" },
                new Passenger { Id = 49, Age = 33, NeedTwoPlaces = false, FamilyName = "P" },
                new Passenger { Id = 50, Age = 3, NeedTwoPlaces = false, FamilyName = "P" },
                new Passenger { Id = 51, Age = 11, NeedTwoPlaces = false, FamilyName = "P" },
                new Passenger { Id = 52, Age = 39, NeedTwoPlaces = false, FamilyName = "A8" },
                new Passenger { Id = 53, Age = 46, NeedTwoPlaces = false, FamilyName = "Q" },
                new Passenger { Id = 54, Age = 10, NeedTwoPlaces = false, FamilyName = "Q" },
                new Passenger { Id = 55, Age = 12, NeedTwoPlaces = false, FamilyName = "Q" },
                new Passenger { Id = 56, Age = 24, NeedTwoPlaces = false, FamilyName = "R" },
                new Passenger { Id = 57, Age = 21, NeedTwoPlaces = false, FamilyName = "R" },
                new Passenger { Id = 58, Age = 2, NeedTwoPlaces = false, FamilyName = "R" },
                new Passenger { Id = 59, Age = 4, NeedTwoPlaces = false, FamilyName = "R" },
                new Passenger { Id = 60, Age = 6, NeedTwoPlaces = false, FamilyName = "R" },
            };
        }
    }
}
