namespace WorkoutMicroservice.Model
{
    public class Workout
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Exercise { get; set; }
        public int Duration { get; set; }
        public string Intensity { get; set; }
        public DateTime Date { get; set; }
    }
}
