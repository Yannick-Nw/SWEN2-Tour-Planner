/*using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.DAL;
using TourPlanner.DAL.Repository;
using Windows.Services.Maps;

namespace TourPlannerTest
{
    [TestFixture]
    public class TourRepositoryTests
    {
        private TourRepository _repository;
        private Mock<TourPlannerContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            // Create a mock for TourPlannerContext
            _mockContext = new Mock<TourPlannerContext>();

            // Initialize the repository with the mock context
            _repository = new TourRepository(_mockContext.Object);
        }

        [Test]
        public async Task AddTourAsync_ShouldSetTourImage()
        {
            // Arrange
            var tour = new Tour { Id = 1, From = "City A", To = "City B" };
            var mapServiceMock = new Mock<MapService>();
            mapServiceMock.Setup(m => m.GetMap(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("/path/to/image.jpg");

            // Act
            await _repository.AddTourAsync(tour);

            // Assert
            Assert.IsNotNullOrEmpty(tour.TourImage);
        }

        [Test]
        public void GetAllTours_ShouldReturnAllTours()
        {
            // Arrange
            var data = new List<Tour>
            {
                new Tour { Id = 1, Name = "Tour 1" },
                new Tour { Id = 2, Name = "Tour 2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Tour>>();
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Tours).Returns(mockSet.Object);

            // Act
            var result = _repository.GetAllTours();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void GetTourById_ShouldReturnTour()
        {
            // Arrange
            var tour = new Tour { Id = 1, Name = "Tour 1" };
            var data = new List<Tour> { tour }.AsQueryable();

            var mockSet = new Mock<DbSet<Tour>>();
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Tours).Returns(mockSet.Object);

            // Act
            var result = _repository.GetTourById(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tour.Id, result.Id);
            Assert.AreEqual(tour.Name, result.Name);
        }

        [Test]
        public void UpdateTour_ShouldUpdateTour()
        {
            // Arrange
            var tour = new Tour { Id = 1, Name = "Tour 1" };
            var data = new List<Tour> { tour }.AsQueryable();

            var mockSet = new Mock<DbSet<Tour>>();
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Tours).Returns(mockSet.Object);

            var updatedTour = new Tour { Id = 1, Name = "Updated Tour" };

            // Act
            _repository.UpdateTour(updatedTour);

            // Assert
            Assert.AreEqual("Updated Tour", tour.Name);
        }

        [Test]
        public void RemoveTour_ShouldRemoveTour()
        {
            // Arrange
            var tour = new Tour { Id = 1, Name = "Tour 1" };
            var data = new List<Tour> { tour }.AsQueryable();

            var mockSet = new Mock<DbSet<Tour>>();
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tour>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(c => c.Tours).Returns(mockSet.Object);

            // Act
            _repository.RemoveTour(tour);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Tour>()), Times.Once);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}
*/