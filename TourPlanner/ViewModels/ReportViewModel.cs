/* using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.Report;
using log4net;

namespace TourPlanner.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ReportViewModel));

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

        public ObservableCollection<Tour> Tours { get; set; }

        public ICommand GenerateTourReportCommand { get; }
        public ICommand GenerateSummaryReportCommand { get; }

        public ReportViewModel()
        {
            // Initialize commands
            GenerateTourReportCommand = new RelayCommand(obj => GenerateTourReport(), obj => SelectedTour != null);
            GenerateSummaryReportCommand = new RelayCommand(obj => GenerateSummaryReport());
        }

        private void GenerateTourReport()
        {
            PdfReportGenerator.GenerateTourReport(SelectedTour);
            logger.Info($"Tour report generated for Tour: {SelectedTour.Name}");
        }

        private void GenerateSummaryReport()
        {
            PdfReportGenerator.GenerateSummaryReport(Tours);
            logger.Info("Summary report generated.");
        }
    }
}
*/

/*
using System;
using System.Collections.Generic;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Report;
using TourPlanner.ViewModels.Abstract;

namespace TourPlanner.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private Tour _selectedTour;
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

        public bool IsSelectedTour => SelectedTour != null;

       
        public RelayCommand GenerateTourReportCommand { get; }

        public ReportViewModel()
        {
           
            GenerateTourReportCommand = new RelayCommand(param => GenerateTourReport(), param => IsSelectedTour);
        }

      

        private void GenerateTourReport()
        {
            if (SelectedTour != null)
            {
                PdfReportGenerator.GenerateTourReport(SelectedTour);
            }
        }
    }
}

*/

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Report;
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

