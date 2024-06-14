using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using System.Threading.Tasks;


namespace TourPlannerTest
{
    [TestFixture]
    public class TourServiceTests
    {
        private ITourService _tourService;
        private ObservableCollection<Tour> _tours;

        [SetUp]
        public void Setup()
        {
            _tours = new ObservableCollection<Tour>();
            _tourService = new TourService();
        }

        [Test]
        public void AddTour_AddsTourToCollection()
        {
            // Arrange
            Tour newTour = new Tour { Name = "Test Tour" };

            // Act
            _tourService.AddTour(_tours, newTour);

            // Assert
            Assert.Contains(newTour, _tours);
        }

        [Test]
        public void UpdateTour_UpdatesTourInCollection()
        {
            // Arrange
            Tour originalTour = new Tour { Name = "Original Tour" };
            Tour updatedTour = new Tour { Name = "Updated Tour" };
            _tours.Add(originalTour);

            // Act
            _tourService.UpdateTour(_tours, originalTour, updatedTour);

            // Assert
            Assert.AreEqual(updatedTour, _tours[0]);
        }

        [Test]
        public void DeleteTour_RemovesTourFromCollection()
        {
            // Arrange
            Tour tourToDelete = new Tour { Name = "Tour to Delete" };
            _tours.Add(tourToDelete);

            // Act
            _tourService.DeleteTour(_tours, tourToDelete);

            // Assert
            Assert.IsFalse(_tours.Contains(tourToDelete));
        }

        [Test]
        public void ExportToursToJson_ExportToursToFile()
        {
            // Arrange
            string filePath = "test_tours.json";
            List<Tour> toursToExport = new List<Tour>
            {
                new Tour { Name = "Tour 1" },
                new Tour { Name = "Tour 2" }
            };

            // Act
            _tourService.ExportToursToJson(toursToExport, filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            // Clean up
            File.Delete(filePath);
        }

        [Test]
        public void ImportToursFromJson_ImportToursFromFile()
        {
            // Arrange
            string filePath = "test_tours.json";
            List<Tour> expectedTours = new List<Tour>
            {
                new Tour { Name = "Tour 1" },
                new Tour { Name = "Tour 2" }
            };
            string jsonData = "[{\"Name\":\"Tour 1\"},{\"Name\":\"Tour 2\"}]";
            File.WriteAllText(filePath, jsonData);

            // Act
            List<Tour> importedTours = _tourService.ImportToursFromJson(filePath);

            // Assert
            Assert.AreEqual(expectedTours.Count, importedTours.Count);
            for (int i = 0; i < expectedTours.Count; i++)
            {
                Assert.AreEqual(expectedTours[i].Name, importedTours[i].Name);
            }
            // Clean up
            File.Delete(filePath);
        }

       

    }
}
