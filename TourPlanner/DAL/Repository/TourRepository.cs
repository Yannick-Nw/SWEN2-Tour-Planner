using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Map;
using System.Diagnostics;
//using Windows.Services.Maps;

namespace TourPlanner.DAL.Repository
{
    public class TourRepository
    {
        private readonly TourPlannerContext context;
        public TourRepository(TourPlannerContext context)
        {
            this.context = context;
        }

        public async Task AddTourAsync(Tour tour)
        {
            MapService mapService = new MapService();
            string filePath = await mapService.GetMap(tour.From, tour.To);
            tour.TourImage = filePath;
            context.Tours.Add(tour);
            context.SaveChanges();
        }

        public List<Tour> GetAllTours()
        {
            return context.Tours.ToList();
        }

        public Tour GetTourById(int id)
        {
            return context.Tours.Find(id);
        }
        public void UpdateTour(Tour tour)
        {
            context.Tours.Update(tour);
            context.SaveChanges();
        }

        public void RemoveTour(Tour tour)
        {
            if (tour == null)
            {
                throw new ArgumentNullException(nameof(tour), "Tour entity cannot be null.");
            }
            //Debug.WriteLine(tour.ToString);

            // Log the ID and state of the Tour entity before attempting to delete
            Debug.WriteLine($"Attempting to remove tour with ID: {tour.Id}");
            Debug.WriteLine($"Tour state before deletion: {context.Entry(tour).State}");

            // Ensure the entity has a permanent ID
            if (tour.Id <= 0)
            {
                throw new InvalidOperationException("The tour entity has wrong ID for deletion.");
            }

            // Retrieve the tour from the database to ensure it is tracked by the context
            var tourToDelete = context.Tours.SingleOrDefault(t => t.Id == tour.Id);
            if (tourToDelete != null)
            {
                context.Tours.Remove(tourToDelete);
                context.SaveChanges();

                // Log success
                Debug.WriteLine($"Successfully removed tour with ID: {tour.Id}");
            }
            else
            {
                throw new InvalidOperationException($"The tour entity with ID {tour.Id} was not found in the database.");
            }
        }
    }
}
