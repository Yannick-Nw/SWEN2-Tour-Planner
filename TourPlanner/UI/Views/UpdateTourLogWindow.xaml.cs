﻿using System.Windows;
using TourPlanner.BusinessLogic.Models;

namespace TourPlanner.UI.Views
{
    public partial class UpdateTourLogWindow : Window
    {
        public TourLog UpdatedTourLog { get; private set; }

        public UpdateTourLogWindow(TourLog tourLog)
        {
            InitializeComponent();
            UpdatedTourLog = tourLog; 
            DataContext = UpdatedTourLog; 
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // Close the window
        }
    }
}

