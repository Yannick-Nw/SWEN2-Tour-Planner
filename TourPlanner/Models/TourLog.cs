using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog : ICloneable
    {
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public string Difficulty { get; set; }
        public double TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

        public object Clone()
        {
            return new TourLog
            {
                DateTime = this.DateTime,
                Comment = this.Comment,
                Difficulty = this.Difficulty,
                TotalDistance = this.TotalDistance,
                TotalTime = this.TotalTime,
                Rating = this.Rating
            };
        }
    }
}
