using Microsoft.EntityFrameworkCore;

namespace FitnessPredictionMicroservice.Model
{
    [Keyless]
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ExerciseName { get; set; }
        public int DurationInMinutes { get; set; }
        public string Intensity { get; set; }
        // Add other properties as needed
    }
}
