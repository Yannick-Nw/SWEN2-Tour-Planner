using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.Views
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
