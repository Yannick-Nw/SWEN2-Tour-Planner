using TourPlanner.Views;
using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Linq;
using log4net;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels;
using Microsoft.Win32;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using TourPlanner.DataAccess.Repository;
using System.Threading.Tasks;

public class TourViewModel : BaseViewModel
{
    private static readonly ILog logger = LogManager.GetLogger(typeof(TourViewModel));
    private string _searchText;
    public string SearchText
    {
        get { return _searchText; }
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(SearchText));
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
    public ObservableCollection<Tour> Tours { get; set; }

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

    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand SearchCommand { get; }
    public ICommand ExportCommand { get; }
    public ICommand ImportCommand { get; }

    string ;

    public TourViewModel()
    {
        Tours = new ObservableCollection<Tour>();

        AddCommand = new RelayCommand(obj => AddTour());
        UpdateCommand = new RelayCommand(obj => UpdateTour(), obj => SelectedTour != null);
        DeleteCommand = new RelayCommand(obj => DeleteTour(), obj => SelectedTour != null);
        SearchCommand = new RelayCommand(obj => Search());
        ExportCommand = new RelayCommand(obj => ExportTours(), obj => SelectedTour != null);
        ImportCommand = new RelayCommand(obj => ImportTours());
        FilteredTours = Tours;

        // Example tours for testing
        Tours.Add(new Tour { Name = "Tour 1", Description = "Description for Tour 1", From = "Starting point for Tour 1", To = "Destination for Tour 1", TransportType = TransportType.Walking, Distance = 100.5, EstimatedTime = TimeSpan.FromHours(2) });
        Tours.Add(new Tour { Name = "Tour 2", Description = "Description for Tour 2", From = "Starting point for Tour 2", To = "Destination for Tour 2", TransportType = TransportType.Bike, Distance = 75.2, EstimatedTime = TimeSpan.FromHours(1.5) });
    }
    public Array TransportTypes => Enum.GetValues(typeof(TransportType));
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
    public void ExportTours()
    {
        if (SelectedTour != null)
        {
            // Show a save file dialog to select the file path for exporting
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Export Tour Data"
            };

            if (dialog.ShowDialog() == true)
            {
                // Create a list to hold only the selected tour
                List<Tour> selectedTourList = new List<Tour> { SelectedTour };

                TourExporter exporter = new TourExporter();
                exporter.ExportToursToJson(selectedTourList, dialog.FileName);

                logger.Info($"Tour data exported to: {dialog.FileName}");
            }
        }

    }
    public void ImportTours()
    {
        // Show a file dialog to select the JSON file to import
        var dialog = new OpenFileDialog
        {
            Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
            Title = "Import Tour Data"
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                // Read the JSON data from the selected file
                string jsonData = File.ReadAllText(dialog.FileName);

                // Deserialize the JSON data into a list of tours
                List<Tour> importedTours = JsonConvert.DeserializeObject<List<Tour>>(jsonData);

                // Add the imported tours to the existing tour collection
                foreach (Tour tour in importedTours)
                {
                    Tours.Add(tour);
                    logger.Info($"Tour imported: {tour.Name}");
                }

                logger.Info($"Tour data imported from: {dialog.FileName}");
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the import process
                logger.Error($"Error importing tour data: {ex.Message}");
            }
        }
    }

    public async Task AddTour()
    {
        var addTourWindow = new AddTourWindow();
        if (addTourWindow.ShowDialog() == true)
        {
            // Add the new tour to the list
            Tours.Add(addTourWindow.NewTour);
            TourRepository addtour = new TourRepository;
            await addtour.AddTourAsync(addTourWindow.NewTour);
            logger.Info($"New Tour added: {addTourWindow.NewTour.Name}");
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
                logger.Info($"Tour updated: {updateTourWindow.UpdatedTour.Name}");
            }
        }
    }

    public void DeleteTour()
    {
        if (SelectedTour != null)
        {
            // Log the deletion attempt
            logger.Info($"Deleting tour: {SelectedTour.Name}");

            // Remove the selected tour from the collection
            Tours.Remove(SelectedTour);
        }
    }
}

