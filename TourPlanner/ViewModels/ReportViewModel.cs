using System.Collections.ObjectModel;
using System.Linq;
using TourPlanner.BusinessLogic.Models;
using TourPlanner.BusinessLogic.Report;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private Tour _selectedTour;
        private ObservableCollection<Tour> _tours;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {
                _selectedTour = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsSelectedTour));
                GenerateTourReportCommand.OnCanExecuteChanged();
            }
        }

        public ObservableCollection<Tour> Tours
        {
            get => _tours;
            set
            {
                _tours = value;
                OnPropertyChanged();
                GenerateSummaryReportCommand.OnCanExecuteChanged();
            }
        }

        public bool IsSelectedTour => SelectedTour != null;

        public RelayCommand GenerateTourReportCommand { get; }
        public RelayCommand GenerateSummaryReportCommand { get; }

        public ReportViewModel()
        {
            GenerateTourReportCommand = new RelayCommand(param => GenerateTourReport(), param => IsSelectedTour);
            GenerateSummaryReportCommand = new RelayCommand(param => GenerateSummaryReport(), param => Tours != null && Tours.Any());
        }

        private void GenerateTourReport()
        {
            if (SelectedTour != null)
            {
                PdfReportGenerator.GenerateTourReport(SelectedTour);
            }
        }

        private void GenerateSummaryReport()
        {
            if (Tours != null && Tours.Any())
            {
                PdfReportGenerator.GenerateSummaryReport(Tours);
            }
        }
    }
}

