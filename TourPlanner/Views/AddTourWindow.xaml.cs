using System.Windows;
using TourPlanner.BusinessLogic.Models;



namespace TourPlanner.Views
{
    public partial class AddTourWindow : Window
    {
        public Tour NewTour { get; private set; }
        public AddTourWindow()
        {
            InitializeComponent();
            NewTour = new Tour(); 
            DataContext = NewTour; 
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

    }
}
