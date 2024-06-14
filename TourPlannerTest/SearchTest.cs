using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.UI.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourViewModelTests
    {
        [Test]
        public void Search_WithValidInput_ShouldFilterTours()
        {
            // Arrange
            var viewModel = new TourViewModel(null); // Pass null since we don't need the service for this test
            viewModel.Tours = new ObservableCollection<Tour>
            {
                new Tour { Name = "Tour 1", Description = "Description for Tour 1" },
                new Tour { Name = "Tour 2", Description = "Description for Tour 2" },
                new Tour { Name = "Another Tour", Description = "Description for Another Tour" }
            };

            // Act
            viewModel.SearchText = "Tour 1";

            // Assert
            Assert.AreEqual(1, viewModel.FilteredTours.Count);
            Assert.IsTrue(viewModel.FilteredTours.All(t => t.Name.Contains("Tour 1")));
        }

        [Test]
        public void Search_WithEmptyInput_ShouldNotFilterTours()
        {
            // Arrange
            var viewModel = new TourViewModel(null); // Pass null since we don't need the service for this test
            viewModel.Tours = new ObservableCollection<Tour>
            {
                new Tour { Name = "Tour 1", Description = "Description for Tour 1" },
                new Tour { Name = "Tour 2", Description = "Description for Tour 2" },
                new Tour { Name = "Another Tour", Description = "Description for Another Tour" }
            };

            // Act
            viewModel.SearchText = "";

            // Assert
            Assert.AreEqual(3, viewModel.FilteredTours.Count);
        }
    }
}
