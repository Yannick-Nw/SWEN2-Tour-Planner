using TourPlanner.Views;
using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TourPlanner.ViewModels
{
    public class TourViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> Tours { get; set; }


        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                OnPropertyChanged(nameof(SelectedTourLog)); 
            }
        }

        
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand AddTourLogCommand { get; }
        public ICommand UpdateTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }

        public TourLog SelectedTourLog { get; set; }
        public TourViewModel()
        {
            // Initialize collection
            Tours = new ObservableCollection<Tour>();
         
            // Initialize commands
            AddCommand = new RelayCommand(obj => AddTour());
            UpdateCommand = new RelayCommand(obj => UpdateTour(), obj => SelectedTour != null);
            DeleteCommand = new RelayCommand(obj => DeleteTour(), obj => SelectedTour != null);
            AddTourLogCommand = new RelayCommand(obj => AddTourLog(), obj => SelectedTour != null);
            UpdateTourLogCommand = new RelayCommand(obj => UpdateTourLog(), obj => SelectedTourLog != null);
            DeleteTourLogCommand = new RelayCommand(obj => DeleteTourLog(), obj => SelectedTourLog != null);
           
      
            // Hardcoded tours for testing
            Tours.Add(new Tour
            {
                Name = "Tour 1",
                Description = "Description for Tour 1",
                From = "Starting point for Tour 1",
                To = "Destination for Tour 1",
                TransportType = "Transportation type for Tour 1",
                Distance = 100.5,
                EstimatedTime = TimeSpan.FromHours(2)
            });

            Tours.Add(new Tour
            {
                Name = "Tour 2",
                Description = "Description for Tour 2",
                From = "Starting point for Tour 2",
                To = "Destination for Tour 2",
                TransportType = "Transportation type for Tour 2",
                Distance = 75.2,
                EstimatedTime = TimeSpan.FromHours(1.5)
            });

            Tours.Add(new Tour
            {
                Name = "Tour 3",
                Description = "Description for Tour 3",
                From = "Starting point for Tour 3",
                To = "Destination for Tour 3",
                TransportType = "Transportation type for Tour 3",
                Distance = 120.8,
                EstimatedTime = TimeSpan.FromHours(3)
            });
        }

         public void AddTourLog()
         {
             var addTourLogWindow = new AddTourLogWindow();
             if (addTourLogWindow.ShowDialog() == true)
             {
                 // Add the new tour log to the selected tour
                 SelectedTour.TourLogs.Add(addTourLogWindow.NewTourLog);
             }
         }
         
        public void UpdateTourLog()
        {
            if (SelectedTourLog != null)
            {
                var updateTourLogWindow = new UpdateTourLogWindow(SelectedTourLog.Clone() as TourLog); // Pass a clone of the selected tour log
                if (updateTourLogWindow.ShowDialog() == true)
                {
                    // Update the tour log in the collection when the user confirms the changes
                    int index = SelectedTour.TourLogs.IndexOf(SelectedTourLog);
                    SelectedTour.TourLogs[index] = updateTourLogWindow.UpdatedTourLog;
                }
            }
        }
        
        public void DeleteTourLog()
        {
            if (SelectedTourLog != null && SelectedTour != null && SelectedTour.TourLogs.Contains(SelectedTourLog))
            {
                // Remove the selected tour log from the collection of the selected tour
                SelectedTour.TourLogs.Remove(SelectedTourLog);
                SelectedTourLog = null; // Clear the selected tour log after deletion
            }
        }
   
        public void AddTour()
        {
            var addTourWindow = new AddTourWindow();
            if (addTourWindow.ShowDialog() == true)
            {
                // Add the new tour to the list
                Tours.Add(addTourWindow.NewTour);
            }
        }


        public void UpdateTour()
        {
            if (SelectedTour != null)
            {
                var updateTourWindow = new UpdateTourWindow(SelectedTour.Clone() as Tour); // Pass a clone of the selected tour
                if (updateTourWindow.ShowDialog() == true)
                {
                    // Update the tour in the collection when the user confirms the changes
                    int index = Tours.IndexOf(SelectedTour);
                    Tours[index] = updateTourWindow.UpdatedTour;
                }
            }
        }

 
        public void DeleteTour()
        {
            Tours.Remove(SelectedTour);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    


    }
}
