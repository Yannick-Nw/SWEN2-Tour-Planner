using System;
using System.ComponentModel.DataAnnotations;

namespace TourPlanner.BusinessLogic.Models
{

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    public class TourLog 
    {
        [Key]
        public int Id { get; private set; }
        private DateTime dateTime = DateTime.UtcNow;
        [Required]
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
        }
        public string Comment { get; set; }
        public Difficulty Difficulty { get; set; }
        public double TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

    
    }
}
