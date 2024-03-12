using TourPlanner.Models;
using TourPlanner.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace TourPlanner
{
    public partial class MainWindow : Window
    {
        private TourViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new TourViewModel(); // Initialize the view model
            DataContext = viewModel; // Set the data context of the MainWindow to the view model
        }

        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            // Create a new tour using the input fields
            Tour newTour = new Tour
            {
                Name = txtTourName.Text,
                Description = txtDescription.Text
            };

            // Add the new tour to the view model
            viewModel.AddTour(newTour);

            // Clear the input fields after adding the tour
            txtTourName.Text = "Tour Name";
            txtDescription.Text = "Description";
        }

        private void DeleteTour_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Tour tour)
            {
                viewModel.Tours.Remove(tour); // Remove the selected tour from the collection
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = ""; // Clear the text when the TextBox gets focus
            }
        }

    }
}
