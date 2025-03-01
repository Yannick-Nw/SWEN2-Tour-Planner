using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using log4net;
using System.Collections.Generic;
using TourPlanner.UI.Views;
using Microsoft.Win32;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.UI.ViewModels.Abstract;
using TourPlanner.DAL;
using TourPlanner.DAL.Repository;

namespace TourPlanner.UI.ViewModels
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

        private TourRepository connection { get; set; }
        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand ImportCommand { get; }
        public ICommand OpenMapCommand { get; }

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

            OpenMapCommand = new RelayCommand(obj => OpenMap());
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
            var dialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Import Tour Data"
            };

            if (dialog.ShowDialog() == true)
            {
                List<Tour> importedTours = _tourService.ImportToursFromJson(dialog.FileName);

                // Save imported tours to database
                foreach (Tour tour in importedTours)
                {
                    // Ensure the imported tour does not already exist in database
                    if (Tours.FirstOrDefault(t => t.Name == tour.Name && t.Description == tour.Description) == null)
                    {
                        connection.AddTourAsync(tour).ContinueWith(task =>
                        {
                            if (task.Exception != null)
                            {
                                // Handle exceptions
                            }
                        });

                        Tours.Add(tour);
                    }
                }
            }
        }

        private void OpenMap()
        {
            if (SelectedTour != null)
            {
                MapWindow mapWindow = new MapWindow();
                mapWindow.DataContext = this; // Set DataContext to this ViewModel
                mapWindow.Show();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

