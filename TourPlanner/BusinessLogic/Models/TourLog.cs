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
        [Required]
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public Difficulty Difficulty { get; set; }
        public double TotalDistance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

    
    }
}
