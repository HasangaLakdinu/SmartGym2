using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationAndMessagingService.Models
{
    public class WorkOutdayInfo
    {
        public int WorkOutdayInfoId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string WorkoutType { get; set; }
        public int WorkoutHours { get; set; }
        public double Weight { get; set; }
        public double BurntCalories { get; set; }
    }
}
