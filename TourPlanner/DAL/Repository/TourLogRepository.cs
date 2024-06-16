using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.Models;
using Windows.UI;

namespace TourPlanner.DAL.Repository
{
    public class TourLogRepository
    {
        private readonly TourPlannerContext context;
        public TourLogRepository(TourPlannerContext context)
        {
            this.context = context;
        }

        public async Task AddTourLogAsync(Tour selectedTour, TourLog tourlog)
        {
            var tour = context.Tours.Include(t => t.TourLogs).FirstOrDefault(t => t.Id == selectedTour.Id);
            if (tour == null)
            {
                throw new InvalidOperationException("Tour not found");
            }

            tour.TourLogs.Add(tourlog);
            context.SaveChanges();
        }

        public List<TourLog> GetAllTourLogs()
        {
            return context.TourLogs.ToList();
        }

        public List<TourLog> GetTourLogsByTourId(int tourId)
        {
            // Query the database for the Tour with its related TourLogs
            var tour = context.Tours.Include(t => t.TourLogs).FirstOrDefault(t => t.Id == tourId);
            if (tour == null)
            {
                throw new InvalidOperationException("Tour not found");
            }
            // Ensure TourLogs collection is initialized
            if (tour.TourLogs == null)
            {
                tour.TourLogs = new List<TourLog>();
            }

            return tour.TourLogs;
           
        }


        public async Task<TourLog> GetTourLogsByIdAsync(int id)
        {
            return await context.TourLogs.FindAsync(id);
        }
        public async Task UpdateTourLogAsync(TourLog updatedTourLog)
        {
            context.TourLogs.Update(updatedTourLog);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTourLogAsync(TourLog tourlog)
        {
            context.TourLogs.Remove(tourlog);
            await context.SaveChangesAsync();
        }
    }
}
