using System.Windows;
using System;
using TourPlanner.Models;
using TourPlanner.ViewModels;



namespace TourPlanner.Views
{
    public partial class AddTourWindow : Window
    {
        public Tour NewTour { get; private set; }
        public AddTourWindow()
        {
            InitializeComponent();
            NewTour = new Tour(); // Initialize a new Tour object
            DataContext = NewTour; // Set the data context to the new Tour object
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            // Create a new tour with the provided information
            Tour newTour = new Tour
            {
                Name = NewTour.Name,
                Description = NewTour.Description,
                From = NewTour.From,
                To = NewTour.To,
                TransportType = NewTour.TransportType,
                Distance = NewTour.Distance,
               
                //EstimatedTime = TimeSpan.FromHours(EstimatedHours) + TimeSpan.FromMinutes(EstimatedMinutes),
                TourImage = NewTour.TourImage
            };

            // Close the window
            DialogResult = true;
        }

    }
}
