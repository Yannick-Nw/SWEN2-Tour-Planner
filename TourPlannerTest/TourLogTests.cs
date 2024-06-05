using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourLogServiceTests
    {
        [Test]
        public void AddTourLog_WhenValidTourLogProvided_ShouldAddTourLogToTour()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLog = new TourLog { DateTime = new System.DateTime(2024, 6, 7), Comment = "Test comment" };
            var tourService = new TourLogService();

            // Act
            tourService.AddTourLog(tour, tourLog);

            // Assert
            Assert.AreEqual(1, tour.TourLogs.Count);
            Assert.IsTrue(tour.TourLogs.Contains(tourLog));
        }

        [Test]
        public void UpdateTourLog_WhenExistingTourLogProvided_ShouldUpdateTourLogInTour()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var originalTourLog = new TourLog { DateTime = new System.DateTime(2024, 6, 7), Comment = "Original comment" };
            var updatedTourLog = new TourLog { DateTime = new System.DateTime(2024, 6, 7), Comment = "Updated comment" };
            tour.TourLogs.Add(originalTourLog);
            var tourService = new TourLogService();

            // Act
            tourService.UpdateTourLog(tour, originalTourLog, updatedTourLog);

            // Assert
            Assert.AreEqual(1, tour.TourLogs.Count);
            Assert.IsTrue(tour.TourLogs.Contains(updatedTourLog));
        }

        [Test]
        public void DeleteTourLog_WhenExistingTourLogProvided_ShouldRemoveTourLogFromTour()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLogToDelete = new TourLog { DateTime = new System.DateTime(2024, 6, 7), Comment = "Delete me" };
            tour.TourLogs.Add(tourLogToDelete);
            var tourService = new TourLogService();

            // Act
            tourService.DeleteTourLog(tour, tourLogToDelete);

            // Assert
            Assert.AreEqual(0, tour.TourLogs.Count);
            Assert.IsFalse(tour.TourLogs.Contains(tourLogToDelete));
        }
    }
}
