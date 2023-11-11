using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessService.Models
{
    [Table("WorkOutdayInfo")]
    public class WorkOutdayInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WorkOutdayInfoId { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public string WorkoutType { get; set; }
        public int WorkoutHours { get; set; }
        public double Weight { get; set; }
        public double BurntCalories { get; set; }
    }
}
