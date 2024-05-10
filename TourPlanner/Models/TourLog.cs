using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }
    public enum Rating
    {
      
       /* OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
       */
       
    }
    public class TourLog : ICloneable
    {
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public Difficulty Difficulty { get; set; }
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
