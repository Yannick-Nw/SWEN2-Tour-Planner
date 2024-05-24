using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TourPlanner.Models
{
    public enum TransportType 
    {
        Walking,
        Bike,
        Car
    }
    public class Tour: ICloneable
    {
       
        [Key]
        public int Id { get; private set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        [Required]
        public TransportType TransportType { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public TimeSpan EstimatedTime { get; set; }
        [Required]
        public string TourImage { get; set; }
        public int Popularity { get; set; }
        public int ChildFriendliness
        {
            get
            {
                if (TourLogs == null || TourLogs.Count == 0)
                    return 0;

                // Calculate average difficulty, total time, and total distance from tour logs
                double avgDifficulty = TourLogs.Average(log => (int)log.Difficulty);
                double totalTime = TourLogs.Sum(log => log.TotalTime.TotalHours);
                double totalDistance = TourLogs.Sum(log => log.TotalDistance);

                // Calculate child-friendliness based on difficulty, time, and distance
                double childFriendliness = (10 - avgDifficulty) * (1 - (totalTime / 24)) * (1 - (totalDistance / 100));

                // Ensure the child-friendliness value is between 0 and 10
                return (int) Math.Max(0, Math.Min(10, childFriendliness));
            }
        }

     
        public List<TourLog> TourLogs { get; set; }
       // public ObservableCollection<TourLog> FilteredTourLogs { get; set; }

        // Constructor
        public Tour()
        {
            TourLogs = [];
            //FilteredTourLogs = new ObservableCollection<TourLog>();
        }

        // Clone method
        public object Clone()
        {
            // Clone existing properties
            var clonedTour = new Tour
            {
                Name = this.Name,
                Description = this.Description,
                From = this.From,
                To = this.To,
                TransportType = this.TransportType,
                Distance = this.Distance,
                EstimatedTime = this.EstimatedTime,
                TourImage = this.TourImage
            };

            // Clone tour logs
            foreach (var tourLog in this.TourLogs)
            {
                clonedTour.TourLogs.Add(new TourLog
                {
                    DateTime = tourLog.DateTime,
                    Comment = tourLog.Comment,
                    Difficulty = tourLog.Difficulty,
                    TotalDistance = tourLog.TotalDistance,
                    TotalTime = tourLog.TotalTime,
                    Rating = tourLog.Rating
                });
            }
           
            return clonedTour;
        }
    }
}

