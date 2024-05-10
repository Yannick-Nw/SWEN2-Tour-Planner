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
            DialogResult = true;
        }

    }
}
