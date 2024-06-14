
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.BusinessLogic.Services
{
    public class TourService : ITourService
    {
        private readonly TourExporter _tourExporter;

        public TourService()
        {
            _tourExporter = new TourExporter();
        }

        public void AddTour(ObservableCollection<Tour> tours, Tour newTour)
        {
            tours.Add(newTour);
        }

        public void UpdateTour(ObservableCollection<Tour> tours, Tour selectedTour, Tour updatedTour)
        {
            int index = tours.IndexOf(selectedTour);
            tours[index] = updatedTour;
        }

        public void DeleteTour(ObservableCollection<Tour> tours, Tour selectedTour)
        {
            tours.Remove(selectedTour);
        }

        public void ExportToursToJson(List<Tour> selectedTours, string filePath)
        {
            _tourExporter.ExportToursToJson(selectedTours, filePath);
        }
        /*
        public List<Tour> ImportToursFromJson(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                List<Tour> importedTours = JsonConvert.DeserializeObject<List<Tour>>(jsonData);
                return importedTours;
            }
            catch
            {
                return new List<Tour>();
            }
        }
        */

        public List<Tour> ImportToursFromJson(string filePath)
        {
            try
            {
                string jsonData = File.ReadAllText(filePath);
                List<Tour> importedTours = JsonConvert.DeserializeObject<List<Tour>>(jsonData);
                return importedTours;
            }
            catch
            {
                return new List<Tour>();
            }
        }
    }
}
