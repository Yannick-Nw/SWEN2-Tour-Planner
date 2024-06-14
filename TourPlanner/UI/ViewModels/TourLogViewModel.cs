using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using log4net;
using TourPlanner.UI.Views;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.UI.ViewModels.Abstract;
using System;

namespace TourPlanner.UI.ViewModels
{
    public class TourLogViewModel : BaseViewModel, INotifyPropertyChanged
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TourLogViewModel));

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
                UpdateTourLogs();
                UpdateTourPopularity();
            }
        }

        private ObservableCollection<TourLog> _tourLogs;
        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            private set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        private ObservableCollection<TourLog> _filteredTourLogs;
        public ObservableCollection<TourLog> FilteredTourLogs
        {
            get { return _filteredTourLogs; }
            set
            {
                _filteredTourLogs = value;
                OnPropertyChanged(nameof(FilteredTourLogs));
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

        private TourLog _selectedTourLog;
        public TourLog SelectedTourLog
        {
            get { return _selectedTourLog; }
            set
            {
                _selectedTourLog = value;
                OnPropertyChanged(nameof(SelectedTourLog));
            }
        }

        public ICommand AddTourLogCommand { get; }
        public ICommand UpdateTourLogCommand { get; }
        public ICommand DeleteTourLogCommand { get; }
        public ICommand SearchCommand { get; }

        private readonly TourLogService _tourLogService;

        public TourLogViewModel()
        {
            _tourLogService = new TourLogService();
            TourLogs = new ObservableCollection<TourLog>();
            // Initialize commands
            SearchCommand = new RelayCommand(obj => Search());
            AddTourLogCommand = new RelayCommand(obj => AddTourLog(), obj => SelectedTour != null);
            UpdateTourLogCommand = new RelayCommand(obj => UpdateTourLog(), obj => SelectedTourLog != null);
            DeleteTourLogCommand = new RelayCommand(obj => DeleteTourLog(), obj => SelectedTourLog != null);

            FilteredTourLogs = TourLogs;
        }

        private void UpdateTourLogs()
        {
            TourLogs.Clear();
            if (SelectedTour?.TourLogs != null)
            {
                foreach (var log in SelectedTour.TourLogs)
                {
                    TourLogs.Add(log);
                }
            }
        }
        private void Search()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredTourLogs = new ObservableCollection<TourLog>(TourLogs.Where(log =>
                    log.Comment.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
            else
            {
                FilteredTourLogs = TourLogs;
            }
        }

        private void UpdateTourPopularity()
        {
            if (SelectedTour != null)
            {
                SelectedTour.Popularity = SelectedTour.TourLogs?.Count ?? 0;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public void AddTourLog()
        {
            var addTourLogWindow = new AddTourLogWindow();
            if (addTourLogWindow.ShowDialog() == true)
            {
                var newTourLog = addTourLogWindow.NewTourLog;
                _tourLogService.AddTourLog(SelectedTour, newTourLog);
                logger.Info($"TourLog added to Tour: {SelectedTour.Name}");

                // Ensure SelectedTour.TourLogs is updated and add the new log to the TourLogs collection only once
                if (!SelectedTour.TourLogs.Contains(newTourLog))
                {
                    SelectedTour.TourLogs.Add(newTourLog);
                }

                // Add the new log to the TourLogs collection only once
                if (!TourLogs.Contains(newTourLog))
                {
                    TourLogs.Add(newTourLog);
                }

                // Notify UI of changes
                OnPropertyChanged(nameof(SelectedTour));
                UpdateTourPopularity();
            }
        }

        public void UpdateTourLog()
        {
            if (SelectedTourLog != null)
            {
                var updateTourLogWindow = new UpdateTourLogWindow(SelectedTourLog);
                if (updateTourLogWindow.ShowDialog() == true)
                {
                    var updatedTourLog = updateTourLogWindow.UpdatedTourLog;
                    _tourLogService.UpdateTourLog(SelectedTour, SelectedTourLog, updatedTourLog);
                    logger.Info($"TourLog updated in Tour: {SelectedTour.Name}");

                    // Update the TourLogs collection and SelectedTour.TourLogs only once
                    int index = TourLogs.IndexOf(SelectedTourLog);
                    if (index >= 0)
                    {
                        TourLogs[index] = updatedTourLog;
                    }
                    int tourIndex = SelectedTour.TourLogs.IndexOf(SelectedTourLog);
                    if (tourIndex >= 0)
                    {
                        SelectedTour.TourLogs[tourIndex] = updatedTourLog;
                    }

                    // Notify UI of changes
                    SelectedTourLog = updatedTourLog;
                    OnPropertyChanged(nameof(SelectedTour));
                    UpdateTourPopularity();
                }
            }
        }

        public void DeleteTourLog()
        {
            if (SelectedTourLog != null && SelectedTour != null && SelectedTour.TourLogs.Contains(SelectedTourLog))
            {
                _tourLogService.DeleteTourLog(SelectedTour, SelectedTourLog);
                logger.Info($"TourLog deleted from Tour: {SelectedTour.Name}");
                SelectedTour.TourLogs.Remove(SelectedTourLog);
                TourLogs.Remove(SelectedTourLog);
                SelectedTourLog = null; // Clear the selected tour log after deletion
                OnPropertyChanged(nameof(SelectedTour));
                UpdateTourPopularity();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
