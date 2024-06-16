using System;
using System.Collections.Generic;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public class TourLogService : ITourLogService
    {
        public void AddTourLog(Tour tour, TourLog newTourLog)
        {
            if (tour == null)
            {
                throw new ArgumentNullException(nameof(tour), "Tour cannot be null.");
            }

            if (newTourLog == null)
            {
                throw new ArgumentNullException(nameof(newTourLog), "Tour log cannot be null.");
            }

            tour.TourLogs.Add(newTourLog);
        }

        public void UpdateTourLog(Tour tour, TourLog selectedTourLog, TourLog updatedTourLog)
        {
            if (tour == null)
            {
                throw new ArgumentNullException(nameof(tour), "Tour cannot be null.");
            }

            if (selectedTourLog == null)
            {
                throw new ArgumentNullException(nameof(selectedTourLog), "Selected tour log cannot be null.");
            }

            if (updatedTourLog == null)
            {
                throw new ArgumentNullException(nameof(updatedTourLog), "Updated tour log cannot be null.");
            }

            int index = tour.TourLogs.IndexOf(selectedTourLog);
            if (index == -1)
            {
                throw new ArgumentException("Selected tour log does not exist in the tour's logs.");
            }

            tour.TourLogs[index] = updatedTourLog;
        }

        public void DeleteTourLog(Tour tour, TourLog selectedTourLog)
        {
            if (tour == null)
            {
                throw new ArgumentNullException(nameof(tour), "Tour cannot be null.");
            }

            if (selectedTourLog == null)
            {
                throw new ArgumentNullException(nameof(selectedTourLog), "Tour log cannot be null.");
            }

            if (!tour.TourLogs.Contains(selectedTourLog))
            {
                throw new ArgumentException("Selected tour log does not exist in the tour's logs.");
            }

            tour.TourLogs.Remove(selectedTourLog);
        }
    }
}

