using NUnit.Framework;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using System;

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
            var tourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Test comment" };
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
            var originalTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Original comment" };
            var updatedTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Updated comment" };
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
            var tourLogToDelete = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Delete me" };
            tour.TourLogs.Add(tourLogToDelete);
            var tourService = new TourLogService();

            // Act
            tourService.DeleteTourLog(tour, tourLogToDelete);

            // Assert
            Assert.AreEqual(0, tour.TourLogs.Count);
            Assert.IsFalse(tour.TourLogs.Contains(tourLogToDelete));
        }

        [Test]
        public void AddTourLog_NullTour_ThrowsArgumentNullException()
        {
            // Arrange
            var tourLogService = new TourLogService();
            Tour tour = null;
            var tourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Test comment" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.AddTourLog(tour, tourLog));
        }

        [Test]
        public void AddTourLog_NullTourLog_ThrowsArgumentNullException()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLogService = new TourLogService();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.AddTourLog(tour, null));
        }

        [Test]
        public void UpdateTourLog_NullTour_ThrowsArgumentNullException()
        {
            // Arrange
            var tourLogService = new TourLogService();
            Tour tour = null;
            var originalTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Original comment" };
            var updatedTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Updated comment" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.UpdateTourLog(tour, originalTourLog, updatedTourLog));
        }

        [Test]
        public void UpdateTourLog_NullSelectedTourLog_ThrowsArgumentNullException()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLogService = new TourLogService();
            TourLog originalTourLog = null;
            var updatedTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Updated comment" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.UpdateTourLog(tour, originalTourLog, updatedTourLog));
        }

        [Test]
        public void UpdateTourLog_NullUpdatedTourLog_ThrowsArgumentNullException()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLogService = new TourLogService();
            var originalTourLog = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Original comment" };
            TourLog updatedTourLog = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.UpdateTourLog(tour, originalTourLog, updatedTourLog));
        }

        [Test]
        public void DeleteTourLog_NullTour_ThrowsArgumentNullException()
        {
            // Arrange
            var tourLogService = new TourLogService();
            Tour tour = null;
            var tourLogToDelete = new TourLog { DateTime = new DateTime(2024, 6, 7), Comment = "Delete me" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.DeleteTourLog(tour, tourLogToDelete));
        }

        [Test]
        public void DeleteTourLog_NullSelectedTourLog_ThrowsArgumentNullException()
        {
            // Arrange
            var tour = new Tour { Name = "Test Tour" };
            var tourLogService = new TourLogService();
            TourLog tourLogToDelete = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => tourLogService.DeleteTourLog(tour, tourLogToDelete));
        }
    }
}
