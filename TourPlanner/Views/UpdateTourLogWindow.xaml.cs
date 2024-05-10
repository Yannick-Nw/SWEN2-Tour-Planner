using System.Windows;
using TourPlanner.Models;

namespace TourPlanner.Views
{
    public partial class UpdateTourLogWindow : Window
    {
        public TourLog UpdatedTourLog { get; private set; }

        public UpdateTourLogWindow(TourLog tourLog)
        {
            InitializeComponent();
            UpdatedTourLog = tourLog; // Set the UpdatedTourLog property to the provided tour log
            DataContext = UpdatedTourLog; // Set the data context to the provided tour log
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Close the window
        }
    }
}

