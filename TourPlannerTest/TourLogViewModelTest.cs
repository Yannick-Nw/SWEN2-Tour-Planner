using NUnit.Framework;
using Moq;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.UI.ViewModels;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourLogViewModelTest
    {
        private Mock<TourLogService> mockService;
        private TourLogViewModel viewModel;

        [SetUp]
        public void Setup()
        {
            mockService = new Mock<TourLogService>();
            viewModel = new TourLogViewModel();
         
        }


        [Test]
        public void AddTourLogCommand_CanExecute_WithoutSelectedTour_ReturnsFalse()
        {
            // Arrange
            viewModel.SelectedTour = null;

            // Act
            bool canExecute = viewModel.AddTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

        [Test]
        public void UpdateTourLogCommand_CanExecute_WithSelectedTourLog_ReturnsTrue()
        {
            // Arrange
            viewModel.SelectedTourLog = new TourLog();

            // Act
            bool canExecute = viewModel.UpdateTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute);
        }

        [Test]
        public void UpdateTourLogCommand_CanExecute_WithoutSelectedTourLog_ReturnsFalse()
        {
            // Arrange
            viewModel.SelectedTourLog = null;

            // Act
            bool canExecute = viewModel.UpdateTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }

    

        [Test]
        public void DeleteTourLogCommand_CanExecute_WithoutSelectedTourLog_ReturnsFalse()
        {
            // Arrange
            viewModel.SelectedTourLog = null;

            // Act
            bool canExecute = viewModel.DeleteTourLogCommand.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute);
        }
    }
}
