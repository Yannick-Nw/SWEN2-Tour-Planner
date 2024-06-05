using NUnit.Framework;
using Moq;
using System.Collections.ObjectModel;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourViewModelTests
    {
        [Test]
        public void AddTourCommand_CanExecute_ReturnsTrue()
        {
            // Arrange
            var mockService = new Mock<TourService>();
            var viewModel = new TourViewModel(mockService.Object);

            // Act
            bool canExecute = viewModel.AddCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void UpdateCommand_WithSelectedTour_CanExecute_ReturnsTrue()
        {
            // Arrange
            var mockService = new Mock<TourService>();
            var viewModel = new TourViewModel(mockService.Object);
            viewModel.SelectedTour = new Tour();

            // Act
            bool canExecute = viewModel.UpdateCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }
    }
}
