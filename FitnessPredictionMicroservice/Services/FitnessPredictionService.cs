using System;
using System.Linq;
using System.Threading.Tasks;
using FitnessPredictionMicroservice.Model;
using FitnessPredictionMicroservice.Models;
using Newtonsoft.Json;

namespace FitnessPredictionMicroservice.Services
{
    public class FitnessPredictionService : IFitnessPredictionService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string apiGatewayUrl = "https://localhost:7278/gateway";

        public async Task<FitnessPrediction> GetFitnessPredictionByUserId(int userId)
        {
            try
            {
                // Get workout history for the user
                var workouts = await GetWorkoutsByUserId(userId);

                // Get cheat meal history for the user
                var cheatMeals = await GetCheatMealsByUserId(userId);

                // Get weight history for the user
                var weightTrackings = await GetWeightTrackingsByUserId(userId);

                // Calculate total workout duration in minutes
                int totalWorkoutDuration = workouts.Sum(w => w.DurationInMinutes);

                // Calculate the count of cheat meals
                int cheatMealCount = cheatMeals.Count();

                // Get the most recent weight data
                var mostRecentWeight = weightTrackings.OrderBy(w => w.Date).LastOrDefault();
                double currentWeight = mostRecentWeight != null ? mostRecentWeight.Value : 0;

                // Calculate the fitness score
                double fitnessScore = CalculateFitnessScore(totalWorkoutDuration, cheatMealCount, currentWeight);

                // Determine fitness status based on the fitness score
                string fitnessStatus = DetermineFitnessStatus(fitnessScore);


                var fitnessPrediction = new FitnessPrediction
                {
                    UserId = userId,
                    FitnessScore = fitnessScore,
                    FitnessStatus = fitnessStatus
                };

                return fitnessPrediction;
            }
            catch (Exception ex)
            {
                // Handle exceptions and log errors
                throw;
            }
        }

        private async Task<List<Workout>> GetWorkoutsByUserId(int userId)
        {
            //fetch workout data for the user
            var response = await _httpClient.GetAsync($"{apiGatewayUrl}/workout/user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var workouts = JsonConvert.DeserializeObject<List<Workout>>(responseBody);
            return workouts;
        }

        private async Task<List<CheatMeal>> GetCheatMealsByUserId(int userId)
        {
            //fetch cheat meal data for the user
            var response = await _httpClient.GetAsync($"{apiGatewayUrl}/cheatmeal/user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var cheatMeals = JsonConvert.DeserializeObject<List<CheatMeal>>(responseBody);
            return cheatMeals;
        }

        private async Task<List<Weight>> GetWeightTrackingsByUserId(int userId)
        {
            // fetch weight tracking data for the user
            var response = await _httpClient.GetAsync($"{apiGatewayUrl}/weight/user/{userId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var weightTrackings = JsonConvert.DeserializeObject<List<Weight>>(responseBody);
            return weightTrackings;
        }

        private double CalculateFitnessScore(int totalWorkoutDuration, int cheatMealCount, double currentWeight)
        {
            // Assign weights to each factor (workout, cheat meal, weight)
            double workoutWeight = 0.6;
            double cheatMealWeight = 0.3;
            double weightWeight = 0.1;

            // Normalize workout duration to a scale of 0 to 100
            double normalizedWorkoutDuration = Math.Min(totalWorkoutDuration / 60.0, 100);

            // Calculate fitness score based on the weighted sum of factors
            double fitnessScore = (workoutWeight * normalizedWorkoutDuration) +
                                  (cheatMealWeight * (1.0 - (cheatMealCount / 10.0))) +
                                  (weightWeight * (100 - Math.Abs(currentWeight - 70)));

            return fitnessScore;
        }

        private string DetermineFitnessStatus(double fitnessScore)
        {
            // Define a threshold value to determine the fitness status
            double fitThreshold = 70.0;

            // Check if the fitness score is above or equal to the threshold
            return fitnessScore >= fitThreshold ? "Fit" : "Unfit";
        }
    }
}
