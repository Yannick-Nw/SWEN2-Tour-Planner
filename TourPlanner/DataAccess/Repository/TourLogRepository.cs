using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccess.Repository
{
    internal class TourLogRepository
    {
        private readonly TourPlannerContext context;
        public TourLogRepository(TourPlannerContext context)
        {
            this.context = context;
        }

        public async Task AddTourLogAsync(TourLog tourlog)
        {
            await context.TourLogs.AddAsync(tourlog);
            await context.SaveChangesAsync();
        }

        public async Task<List<TourLog>> GetAllTourLogsAsync()
        {
            return await context.TourLogs.ToListAsync();
        }

        public async Task<TourLog> GetTourLogsByIdAsync(int id)
        {
            return await context.TourLogs.FindAsync(id);
        }
        public async Task UpdateTourLogAsync(TourLog tourlog)
        {
            context.TourLogs.Update(tourlog);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTourLogAsync(TourLog tourlog)
        {
            context.TourLogs.Remove(tourlog);
            await context.SaveChangesAsync();
        }
    }
}
