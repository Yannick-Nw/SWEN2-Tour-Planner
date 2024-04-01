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
        public List<TourLog> Logs { get; set; } // New property

        public object Clone()
        {
            return new Tour
            {
                Name = this.Name,
                Description = this.Description,
                From = this.From,
                To = this.To,
                TransportType = this.TransportType,
                Distance = this.Distance,
                EstimatedTime = this.EstimatedTime,
                Logs = this.Logs.Select(log => (TourLog)log.Clone()).ToList() // Clone each log
            };
        }
    }
}
