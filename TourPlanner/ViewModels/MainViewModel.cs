using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public TourViewModel TourViewModel { get; }
        public TourLogViewModel TourLogViewModel { get; }
        public ReportViewModel ReportViewModel { get; }

        public MainViewModel()
        {
            TourViewModel = new TourViewModel();
            TourLogViewModel = new TourLogViewModel { SelectedTour = TourViewModel.SelectedTour };
            ReportViewModel = new ReportViewModel
            {
                SelectedTour = TourViewModel.SelectedTour,
                Tours = TourViewModel.Tours
            };

            TourViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(TourViewModel.SelectedTour))
                {
                    TourLogViewModel.SelectedTour = TourViewModel.SelectedTour;
                    ReportViewModel.SelectedTour = TourViewModel.SelectedTour;
                }
                else if (args.PropertyName == nameof(TourViewModel.Tours))
                {
                    ReportViewModel.Tours = TourViewModel.Tours;
                }
            };
        }
    }
}
