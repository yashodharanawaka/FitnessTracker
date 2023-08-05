using Microsoft.EntityFrameworkCore;

namespace FitnessPredictionMicroservice.Model
{
    [Keyless]
    public class CheatMeal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
    }
}
