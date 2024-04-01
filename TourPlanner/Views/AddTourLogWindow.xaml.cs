using System;
using System.Windows;
using System.Windows.Controls;
using TourPlanner.Models;

namespace TourPlanner.Views
{
    public partial class AddTourLogWindow : Window
    {
        public TourLog NewLog { get; private set; }

        public AddTourLogWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new TourLog from the input fields
            NewLog = new TourLog
            {
                DateTime = datePicker.SelectedDate.Value,
                Comment = commentTextBox.Text,
                Difficulty = difficultyTextBox.Text,
                TotalDistance = double.Parse(totalDistanceTextBox.Text),
                TotalTime = TimeSpan.Parse(totalTimeTextBox.Text),
                Rating = int.Parse(ratingTextBox.Text)
            };

            // Close the window and set the DialogResult to true
            DialogResult = true;
        }
    }
}
