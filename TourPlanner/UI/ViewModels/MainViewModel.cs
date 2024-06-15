
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Services;
using TourPlanner.UI.ViewModels.Abstract;

namespace TourPlanner.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        private ObservableCollection<Tour> _recommendedTours;
        public ObservableCollection<Tour> RecommendedTours
        {
            get { return _recommendedTours; }
            set
            {
                _recommendedTours = value;
                OnPropertyChanged(nameof(RecommendedTours));
            }
        }

        public ICommand OpenMapCommand { get; private set; }
        private TourViewModel _tourViewModel;
        private TourLogViewModel _tourLogViewModel;
        private ReportViewModel _reportViewModel;
        
        public MainViewModel()
        {
           
            TourService tourService = new TourService(); // Create an instance of TourService
            _tourViewModel = new TourViewModel(tourService); // Pass tourService to TourViewModel constructor
            _tourLogViewModel = new TourLogViewModel { SelectedTour = _tourViewModel.SelectedTour };
            _reportViewModel = new ReportViewModel
            {
                SelectedTour = _tourViewModel.SelectedTour,
                Tours = _tourViewModel.Tours
            };

            _tourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(_tourViewModel.SelectedTour))
                {
                    _tourLogViewModel.SelectedTour = _tourViewModel.SelectedTour;
                    _reportViewModel.SelectedTour = _tourViewModel.SelectedTour;
                }
                else if (args.PropertyName == nameof(_tourViewModel.Tours))
                {
                    _reportViewModel.Tours = _tourViewModel.Tours;
                }
                // Update recommended tours when Tours collection changes
                UpdateRecommendedTours();
            };
            UpdateRecommendedTours();
        }

        private void UpdateRecommendedTours()
        {
            // Order tours by popularity and take top 3
            var topTours = _tourViewModel.Tours.OrderByDescending(t => t.Popularity).Take(3);
            RecommendedTours = new ObservableCollection<Tour>(topTours);
        }
        public TourViewModel TourViewModel
        {
            get { return _tourViewModel; }
            set
            {
                _tourViewModel = value;
                OnPropertyChanged();
            }
        }

        public TourLogViewModel TourLogViewModel
        {
            get { return _tourLogViewModel; }
            set
            {
                _tourLogViewModel = value;
                OnPropertyChanged();
            }
        }

        public ReportViewModel ReportViewModel
        {
            get { return _reportViewModel; }
            set
            {
                _reportViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}

