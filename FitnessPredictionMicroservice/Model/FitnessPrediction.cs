using Microsoft.EntityFrameworkCore;

namespace FitnessPredictionMicroservice.Models
{
    [Keyless]
    public class FitnessPrediction
    {
        public int UserId { get; set; }
        public double FitnessScore { get; set; }
        public string FitnessStatus { get; set; }
    }
}
