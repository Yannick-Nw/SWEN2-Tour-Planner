using System.Collections.Generic;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public class TourLogService : ITourLogService
    {
        public void AddTourLog(Tour tour, TourLog newTourLog)
        {
            tour.TourLogs.Add(newTourLog);
        }

        public void UpdateTourLog(Tour tour, TourLog selectedTourLog, TourLog updatedTourLog)
        {
            int index = tour.TourLogs.IndexOf(selectedTourLog);
            tour.TourLogs[index] = updatedTourLog;
        }

        public void DeleteTourLog(Tour tour, TourLog selectedTourLog)
        {
            tour.TourLogs.Remove(selectedTourLog);
        }
    }
}
