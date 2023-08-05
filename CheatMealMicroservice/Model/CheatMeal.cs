namespace CheatMealMicroservice.Models
{
    public class CheatMeal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
    }
}
