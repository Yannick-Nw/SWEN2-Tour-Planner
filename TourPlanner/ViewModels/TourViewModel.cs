
using TourPlanner.Views;
using TourPlanner.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TourPlanner.DataAccess;
using Microsoft.EntityFrameworkCore;
using TourPlanner.logging;
using System.Reflection;
using log4net;
using TourPlanner.Report;
using TourPlanner.ViewModels.Abstract;
using TourPlanner.ViewModels;


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

    public TourViewModel()
    {
        Tours = new ObservableCollection<Tour>();

        AddCommand = new RelayCommand(obj => AddTour());
        UpdateCommand = new RelayCommand(obj => UpdateTour(), obj => SelectedTour != null);
        DeleteCommand = new RelayCommand(obj => DeleteTour(), obj => SelectedTour != null);
        SearchCommand = new RelayCommand(obj => Search());

        FilteredTours = Tours;

        // Hardcoded tours for testing
        Tours.Add(new Tour { Name = "Tour 1", Description = "Description for Tour 1", From = "Starting point for Tour 1", To = "Destination for Tour 1", TransportType = "Transportation type for Tour 1", Distance = 100.5, EstimatedTime = TimeSpan.FromHours(2) });
        Tours.Add(new Tour { Name = "Tour 2", Description = "Description for Tour 2", From = "Starting point for Tour 2", To = "Destination for Tour 2", TransportType = "Transportation type for Tour 2", Distance = 75.2, EstimatedTime = TimeSpan.FromHours(1.5) });
        Tours.Add(new Tour { Name = "Tour 3", Description = "Description for Tour 3", From = "Starting point for Tour 3", To = "Destination for Tour 3", TransportType = "Transportation type for Tour 3", Distance = 120.8, EstimatedTime = TimeSpan.FromHours(3) });
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
   
    public void AddTour()
        {
            var addTourWindow = new AddTourWindow();
            if (addTourWindow.ShowDialog() == true)
            {
                // Add the new tour to the list
                Tours.Add(addTourWindow.NewTour);
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

