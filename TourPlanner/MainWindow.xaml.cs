using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;
using System.Windows.Media;
using TourPlanner.ViewModels;



namespace TourPlanner
{
    
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
           // DataContext = new TourViewModel();

           
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                ContextMenu contextMenu = this.FindResource("ExportContextMenu") as ContextMenu;
                if (contextMenu != null)
                {
                    contextMenu.PlacementTarget = button;
                    contextMenu.IsOpen = true;
                }
            }
        }


    }

}