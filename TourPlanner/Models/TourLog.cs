using System;
using System.ComponentModel.DataAnnotations;

namespace TourPlanner.Models
{

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class TourLog : ICloneable
    {
        [Key]
        public int Id { get; private set; }
        [Required]
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
