using System;
using System.Windows;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.UI.Views
{
    public partial class AddTourLogWindow : Window
    {
        public TourLog NewTourLog { get; private set; }

        public AddTourLogWindow()
        {
            InitializeComponent();
            NewTourLog = new TourLog();
            DataContext = NewTourLog;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
