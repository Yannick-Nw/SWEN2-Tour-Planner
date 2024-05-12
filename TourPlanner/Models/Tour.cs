using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
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
        public string TransportType { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public TimeSpan EstimatedTime { get; set; }
        [Required]
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