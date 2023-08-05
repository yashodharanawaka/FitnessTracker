namespace WeightTrackingMicroservice.Models
{
    public class Weight
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
