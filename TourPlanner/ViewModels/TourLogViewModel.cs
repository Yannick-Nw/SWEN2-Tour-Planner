
using System.Collections.ObjectModel;
using System.Windows.Input;
using log4net;
using TourPlanner.Views;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
    public class TourLogViewModel : BaseViewModel
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
                OnPropertyChanged(nameof(TourLogs));
                UpdateTourPopularity();
            }
        }

        public ObservableCollection<TourLog> TourLogs => SelectedTour?.TourLogs != null
            ? new ObservableCollection<TourLog>(SelectedTour.TourLogs)
            : null;

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

        private readonly TourLogService _tourLogService;

        public TourLogViewModel()
        {
            _tourLogService = new TourLogService();

            // Initialize commands
            AddTourLogCommand = new RelayCommand(obj => AddTourLog(), obj => SelectedTour != null);
            UpdateTourLogCommand = new RelayCommand(obj => UpdateTourLog(), obj => SelectedTourLog != null);
            DeleteTourLogCommand = new RelayCommand(obj => DeleteTourLog(), obj => SelectedTourLog != null);
            UpdateTourPopularity();
        }

        private void UpdateTourPopularity()
        {
            if (SelectedTour != null)
            {
                SelectedTour.Popularity = SelectedTour.TourLogs?.Count ?? 0;
                // Notify UI of the change
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public void AddTourLog()
        {
            var addTourLogWindow = new AddTourLogWindow();
            if (addTourLogWindow.ShowDialog() == true)
            {
                _tourLogService.AddTourLog(SelectedTour, addTourLogWindow.NewTourLog);
                logger.Info($"TourLog added to Tour: {SelectedTour.Name}");
                OnPropertyChanged(nameof(TourLogs));
            }
        }

        public void UpdateTourLog()
        {
            if (SelectedTourLog != null)
            {
                var updateTourLogWindow = new UpdateTourLogWindow(SelectedTourLog.Clone() as TourLog); // Pass a clone of the selected tour log
                if (updateTourLogWindow.ShowDialog() == true)
                {
                    _tourLogService.UpdateTourLog(SelectedTour, SelectedTourLog, updateTourLogWindow.UpdatedTourLog);
                    logger.Info($"TourLog updated in Tour: {SelectedTour.Name}");
                    OnPropertyChanged(nameof(TourLogs));
                }
            }
        }

        public void DeleteTourLog()
        {
            if (SelectedTourLog != null && SelectedTour != null && SelectedTour.TourLogs.Contains(SelectedTourLog))
            {
                _tourLogService.DeleteTourLog(SelectedTour, SelectedTourLog);
                logger.Info($"TourLog deleted from Tour: {SelectedTour.Name}");
                SelectedTourLog = null; // Clear the selected tour log after deletion
                OnPropertyChanged(nameof(TourLogs));
                OnPropertyChanged(nameof(SelectedTourLog));
            }
        }
    }
}
