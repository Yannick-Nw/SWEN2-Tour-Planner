using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Views
{
    public partial class AddTourLogWindow : Window
    {
        public AddTourLogWindow()
        {
            InitializeComponent();
            DataContext = new TourViewModel();
        }
    }
}
