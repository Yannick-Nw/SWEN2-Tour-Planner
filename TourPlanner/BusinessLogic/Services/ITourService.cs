using System.Collections.Generic;
using System.Collections.ObjectModel;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public interface ITourService
    {
        void AddTour(ObservableCollection<Tour> tours, Tour newTour);
        void UpdateTour(ObservableCollection<Tour> tours, Tour selectedTour, Tour updatedTour);
        void DeleteTour(ObservableCollection<Tour> tours, Tour selectedTour);
        void ExportToursToJson(List<Tour> selectedTours, string filePath);
        List<Tour> ImportToursFromJson(string filePath);
    }
}
