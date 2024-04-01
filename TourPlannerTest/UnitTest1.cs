using NUnit.Framework;
using TourPlanner.ViewModels;
using TourPlanner.Models;
using System;
namespace TourPlannerTest
{
    public class Tests
    {
        [TestFixture]
        public class TourViewModelTests
        {
            [Test]
            public void AddCommand_WhenCalled_AddsNewTour()
            {
                // Arrange
                var viewModel = new TourViewModel();
                var initialCount = viewModel.Tours.Count;

                // Act
                viewModel.AddCommand.Execute(null);

                // Assert
                Assert.AreEqual(initialCount + 1, viewModel.Tours.Count);
            }

            [Test]
            public void UpdateCommand_WhenCalled_UpdatesTour()
            {
                // Arrange
                var viewModel = new TourViewModel();
                var tour = new Tour(); // Create a sample tour
                viewModel.Tours.Add(tour);
                var newName = "New Tour Name";

                // Act
                tour.Name = newName;
                viewModel.UpdateCommand.Execute(null);

                // Assert
                Assert.AreEqual(newName, tour.Name);
            }
            [Test]
            public void AddTour_WhenCalledWithValidInput_AddsTourToList()
            {
                // Arrange
                var viewModel = new TourViewModel();
                var initialCount = viewModel.Tours.Count;
                var newTour = new Tour
                {
                    Name = "Test Tour",
                    Description = "Test description",
                    From = "Test start location",
                    To = "Test destination",
                    TransportType = "Test transport type",
                    Distance = 50.0,
                    EstimatedTime = TimeSpan.FromHours(1)
                };

                // Act
                viewModel.AddCommand.Execute(null);

                // Assert
                Assert.AreEqual(initialCount + 1, viewModel.Tours.Count);
                Assert.IsTrue(viewModel.Tours.Contains(newTour));
            }

            [Test]
            public void DeleteCommand_WhenCalled_RemovesTour()
            {
                // Arrange
                var viewModel = new TourViewModel();
                var tour = new Tour(); // Create a sample tour
                viewModel.Tours.Add(tour);
                var initialCount = viewModel.Tours.Count;

                // Act
                viewModel.SelectedTour = tour;
                viewModel.DeleteCommand.Execute(null);

                // Assert
                Assert.AreEqual(initialCount - 1, viewModel.Tours.Count);
                Assert.IsFalse(viewModel.Tours.Contains(tour));

            }
        }
    }
}