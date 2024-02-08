using Microsoft.Extensions.Logging;
using Moq;
using PassengerManagement.Entities;
using PassengerManagement.Services;
using System.Collections.Generic;
using Xunit;

namespace PassengerManagement.Tests
{
    public class CheckRulesAndGetFamiliesTests
    {
        [Fact]
        public void CheckRulesAndGetFamilies_ReturnsEmptyList_WhenPassengersIsNull()
        {
            var logger = new Mock<ILogger<IPassengerManagementService>>().Object;
            var service = new PassengerManagementService(logger);

            // Arrange
            List<Passenger> passengers = null;
          
            // Act
            var result = service.CheckRulesAndGetFamilies(passengers);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void CheckRulesAndGetFamilies_ReturnsEmptyList_WhenPassengersIsEmpty()
        {
            var logger = new Mock<ILogger<IPassengerManagementService>>().Object;
            var service = new PassengerManagementService(logger);

            // Arrange
            List<Passenger> passengers = new List<Passenger>();

            // Act
            var result = service.CheckRulesAndGetFamilies(passengers);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void CheckRulesAndGetFamilies_ReturnsFamiliesWithValidMembers()
        {
            var logger = new Mock<ILogger<IPassengerManagementService>>().Object;
            var service = new PassengerManagementService(logger);

            // Arrange
            List<Passenger> passengers = new List<Passenger>
            {
                new Passenger { Id = 1, Age = 35, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 2, Age = 32, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 3, Age = 7, NeedTwoPlaces = false, FamilyName = "A" },
                new Passenger { Id = 3, Age = 7, NeedTwoPlaces = true, FamilyName = "B" },
            };

            // Act
            var result = service.CheckRulesAndGetFamilies(passengers);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}
