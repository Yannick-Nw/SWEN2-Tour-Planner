
using System.Windows;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.UI.Views
{
    /// <summary>
    /// Interaktionslogik für UpdateTourWindow.xaml
    /// </summary>
    public partial class UpdateTourWindow : Window
    {
        public Tour UpdatedTour { get; private set; }

        public UpdateTourWindow(Tour tour)
        {
            InitializeComponent();
            UpdatedTour = tour; // Set the UpdatedTour property to the provided tour
            DataContext = UpdatedTour; // Set the data context to the provided tour
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Close the window
        }

    }

}
