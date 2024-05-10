using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class Tour: ICloneable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public string TourImage { get; set; }

        public List<TourLog> TourLogs { get; set; }

        // Constructor
        public Tour()
        {
            TourLogs = new List<TourLog>();
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