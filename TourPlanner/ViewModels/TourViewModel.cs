using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

using log4net;
using System.Collections.Generic;
using TourPlanner.Views;
using Microsoft.Win32;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.ViewModels.Abstract;
using Windows.Networking.Connectivity;
using TourPlanner.DataAccess;
using TourPlanner.DataAccess.Repository;


namespace TourPlanner.ViewModels
{
    public class TourViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TourViewModel));

        private TourService _tourService;

        private ObservableCollection<Tour> _tours;
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        private ObservableCollection<Tour> _filteredTours;
        public ObservableCollection<Tour> FilteredTours
        {
            get { return _filteredTours; }
            set
            {
                _filteredTours = value;
                OnPropertyChanged(nameof(FilteredTours));
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search(); // Trigger search whenever SearchText changes
            }
        }

        //private TourPlannerContext context { get; set; }
        private TourRepository connection { get; set; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand SearchCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public TourViewModel(TourService tourService)
        {
            _tourService = tourService;

            // Initialize commands
            TourPlannerContext context = new TourPlannerContext();
            connection = new TourRepository(context);

            AddCommand = new RelayCommand(obj => AddTour());
            UpdateCommand = new RelayCommand(obj => UpdateTour(), obj => SelectedTour != null);
            DeleteCommand = new RelayCommand(obj => DeleteTour(), obj => SelectedTour != null);

            SearchCommand = new RelayCommand(obj => Search());
            ExportCommand = new RelayCommand(obj => ExportTours(), obj => SelectedTour != null);
            ImportCommand = new RelayCommand(obj => ImportTours());
            /*
            // Example tours for testing
            Tours = new ObservableCollection<Tour>();
     
            List<Tour> dbtours = connection.GetAllTours();
            foreach (Tour tour in dbtours)
            {
                Tours.Add(new Tour
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    From = tour.From,
                    To = tour.To,
                    TransportType = tour.TransportType,
                    Distance = tour.Distance,
                    EstimatedTime = tour.EstimatedTime,
                    TourImage = tour.TourImage,
                    Popularity = tour.Popularity,
                    TourLogs = tour.TourLogs // Assuming deep copy or reference is acceptable here
                });
            }
            */


            // Load tours from database
            Tours = new ObservableCollection<Tour>(connection.GetAllTours());

            // Initialize FilteredTours with all tours
            FilteredTours = Tours;
           
        }


        private void AddTour()
        {
            var addTourWindow = new AddTourWindow();
            if (addTourWindow.ShowDialog() == true)
            {
                logger.Info("Adding new tour: " + addTourWindow.NewTour.Name);

                // Call asynchronously without waiting
                connection.AddTourAsync(addTourWindow.NewTour).ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        // Iterate through all AggregateExceptions
                        foreach (var ex in task.Exception.InnerExceptions)
                        {
                            logger.Error("An error occurred while adding the tour: " + ex.Message);
                            // Here we could also add user notification
                        }
                        throw new InvalidOperationException("An error occurred while adding the tour.");
                    }
                });

                _tourService.AddTour(Tours, addTourWindow.NewTour);
            }
        }



        private void UpdateTour()
        {
            var updateTourWindow = new UpdateTourWindow(SelectedTour);
            if (updateTourWindow.ShowDialog() == true)
            {
                logger.Info("Updating tour: " + SelectedTour.Name);
                connection.UpdateTour(SelectedTour);
                _tourService.UpdateTour(Tours, SelectedTour, updateTourWindow.UpdatedTour);
            }
        }

        private void DeleteTour()
        {
            logger.Info("Deleting tour: " + SelectedTour.Name);
            connection.RemoveTour(SelectedTour);
            _tourService.DeleteTour(Tours, SelectedTour);
        }

        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredTours = new ObservableCollection<Tour>(Tours.Where(t => t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                FilteredTours = Tours;
            }
        }

        private void ExportTours()
        {
            logger.Info("Exporting tour: " + SelectedTour.Name);
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Export Tour Data"
            };

            if (dialog.ShowDialog() == true)
            {
                _tourService.ExportToursToJson(new List<Tour> { SelectedTour }, dialog.FileName);
            }
        }

        private void ImportTours()
        {
            logger.Info("Importing tours.");
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Import Tour Data"
            };

            if (dialog.ShowDialog() == true)
            {
                List<Tour> importedTours = _tourService.ImportToursFromJson(dialog.FileName);
                foreach (Tour tour in importedTours)
                {
                    Tours.Add(tour);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

