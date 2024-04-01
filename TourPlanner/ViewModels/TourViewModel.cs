using TourPlanner.Views;
using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;

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

                // Update TourLogs whenever SelectedTour changes
                TourLogs = new ObservableCollection<TourLog>(_selectedTour?.Logs);
            }
        }


        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }


        private ObservableCollection<TourLog> _tourLogs;

        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public TourLog SelectedTourLog { get; set; }

        public ICommand AddLogCommand { get; }
        public ICommand UpdateLogCommand { get; }
        public ICommand DeleteLogCommand { get; }

        public class RelayCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
            public void Execute(object parameter) => _execute(parameter);

            // Event required by ICommand interface
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }

        public TourViewModel()
        {
            // Initialize commands
            AddCommand = new RelayCommand(obj => AddTour());
            UpdateCommand = new RelayCommand(obj => UpdateTour(), obj => SelectedTour != null);
            DeleteCommand = new RelayCommand(obj => DeleteTour(), obj => SelectedTour != null);

            // Initialize collection
            Tours = new ObservableCollection<Tour>();

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

            // Initialize TourLog commands
            AddLogCommand = new RelayCommand(obj => AddTourLog(), obj => SelectedTour != null);
            UpdateLogCommand = new RelayCommand(obj => UpdateTourLog(), obj => SelectedTourLog != null);
            DeleteLogCommand = new RelayCommand(obj => DeleteTourLog(), obj => SelectedTourLog != null);

            // Initialize TourLog collection
            TourLogs = new ObservableCollection<TourLog>();
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

        public void AddTourLog()
        {
            var addLogWindow = new AddTourLogWindow();
            if (addLogWindow.ShowDialog() == true)
            {
                // Add the new log to the selected tour's logs
                SelectedTour.Logs.Add(addLogWindow.NewLog);

                // Update the TourLogs collection
                TourLogs = new ObservableCollection<TourLog>(SelectedTour.Logs);
            }
        }

        public void UpdateTourLog()
        {
            if (SelectedTourLog != null)
            {
                var updateLogWindow = new UpdateTourLogWindow(SelectedTourLog.Clone() as TourLog); // Pass a clone of the selected log
                if (updateLogWindow.ShowDialog() == true)
                {
                    // Update the log in the collection when the user confirms the changes
                    int index = SelectedTour.Logs.IndexOf(SelectedTourLog);
                    SelectedTour.Logs[index] = updateLogWindow.UpdatedLog;

                    // Update the TourLogs collection
                    TourLogs = new ObservableCollection<TourLog>(SelectedTour.Logs);
                }
            }
        }

        public void DeleteTourLog()
        {
            SelectedTour.Logs.Remove(SelectedTourLog);

            // Update the TourLogs collection
            TourLogs = new ObservableCollection<TourLog>(SelectedTour.Logs);
        }


    }
}
