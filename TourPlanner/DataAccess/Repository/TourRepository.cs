using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Map;
//using Windows.Services.Maps;

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
            context.Tours.Remove(tour);
            context.SaveChanges();
        }
    }
}
