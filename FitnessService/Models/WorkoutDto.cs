namespace FitnessService.Models
{
    public class WorkoutDto
    {
        public string WorkoutType { get; set; }
        public int WorkoutHours { get; set; }
        public double Weight { get; set; }
        public double BurntCalories { get; set; }
    }
}
