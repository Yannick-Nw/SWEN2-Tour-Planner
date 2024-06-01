using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.DataAccess.Repository
{
    internal class TourRepository
    {
        private readonly TourPlannerContext context;
        public TourRepository(TourPlannerContext context)
        {
            this.context = context;
        }

        public async Task AddTourAsync(Tour tour)
        {
            await context.Tours.AddAsync(tour);
            await context.SaveChangesAsync();
        }

        public async Task<List<Tour>> GetAllToursAsync()
        {
            return await context.Tours.ToListAsync();
        }

        public async Task<Tour> GetTourByIdAsync(int id)
        {
            return await context.Tours.FindAsync(id);
        }
        public async Task UpdateTourAsync(Tour tour)
        {
            context.Tours.Update(tour);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTourAsync(Tour tour)
        {
            context.Tours.Remove(tour);
            await context.SaveChangesAsync();
        }
    }
}
