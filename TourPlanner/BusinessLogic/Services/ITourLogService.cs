using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public interface ITourLogService
    {
        void AddTourLog(Tour tour, TourLog newTourLog);
        void UpdateTourLog(Tour tour, TourLog selectedTourLog, TourLog updatedTourLog);
        void DeleteTourLog(Tour tour, TourLog selectedTourLog);
    }
}
