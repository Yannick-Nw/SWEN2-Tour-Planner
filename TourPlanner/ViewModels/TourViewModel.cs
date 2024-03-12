
using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> Tours { get; set; }

        // Constructor
        public TourViewModel()
        {
            // Initialize collection
            Tours = new ObservableCollection<Tour>();
        }

        public void AddTour(Tour tour)
        {
            Tours.Add(tour);
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
