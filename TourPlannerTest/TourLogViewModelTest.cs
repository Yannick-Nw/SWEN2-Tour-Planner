using NUnit.Framework;
using Moq;
using System.Collections.ObjectModel;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourLogViewModelTest
    {
        [Test]
        public void AddTourLogCommand_CanExecute_WithSelectedTour_ReturnsTrue()
        {
            // Arrange
            var mockService = new Mock<TourLogService>();
            var viewModel = new TourLogViewModel();
            viewModel.SelectedTour = new Tour();

            // Act
            bool canExecute = viewModel.AddTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void UpdateTourLogCommand_CanExecute_WithSelectedTourLog_ReturnsTrue()
        {
            // Arrange
            var mockService = new Mock<TourLogService>();
            var viewModel = new TourLogViewModel();
            viewModel.SelectedTourLog = new TourLog();

            // Act
            bool canExecute = viewModel.UpdateTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void DeleteTourLogCommand_CanExecute_WithSelectedTourLog_ReturnsTrue()
        {
            // Arrange
            var mockService = new Mock<TourLogService>();
            var viewModel = new TourLogViewModel();
            viewModel.SelectedTour = new Tour();
            viewModel.SelectedTourLog = new TourLog();

            // Act
            bool canExecute = viewModel.DeleteTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

       
    }
}
