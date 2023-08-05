using Microsoft.EntityFrameworkCore;
using WeightTrackingMicroservice.Data;
using WeightTrackingMicroservice.Models;

namespace WeightTrackingMicroservice.Services
{
    public class WeightService : IWeightService
    {
        private readonly DbContextClass _dbContext;

        public WeightService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Weight>> GetWeightsByUserId(int userId)
        {
            return await _dbContext.Weights
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<Weight> GetWeightById(int id)
        {
            return await _dbContext.Weights.FindAsync(id);
        }

        public async Task<Weight> AddWeight(Weight weight)
        {
            _dbContext.Weights.Add(weight);
            await _dbContext.SaveChangesAsync();
            return weight;
        }

        public async Task<Weight> UpdateWeight(int id, Weight weight)
        {
            var existingWeight = await _dbContext.Weights.FindAsync(id);
            if (existingWeight == null)
            {
                return null;
            }

            existingWeight.Date = weight.Date;
            existingWeight.Value = weight.Value;

            await _dbContext.SaveChangesAsync();
            return existingWeight;
        }

        public async Task<bool> DeleteWeight(int id)
        {
            var weight = await _dbContext.Weights.FindAsync(id);
            if (weight == null)
            {
                return false;
            }

            _dbContext.Weights.Remove(weight);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<double> CalculateNextWeight(int userId)
        {
            // Fetch historical weight data for the given user ID
            List<Weight> userWeightData = (await GetWeightsByUserId(userId)).ToList();

            if (userWeightData == null || userWeightData.Count == 0)
            {
                // Return a default value (e.g., -1) when there is no weight data available for the user
                return -1;
            }

            // Check if there is enough data to make a prediction
            if (userWeightData.Count == 1)
            {
                return userWeightData[0].Value; // Return the only entered weight as the prediction
            }

            // Perform simple linear regression to predict the next weight

            // Calculate the total number of data points
            int n = userWeightData.Count;

            // Default prediction logic for cases with few data points
            if (userWeightData.Count <= 3)
            {
                // Calculate the average weight
                double totalWeight = userWeightData.Sum(data => data.Value);
                return totalWeight / userWeightData.Count;
            }

            // Calculate the sum of x (dates in days)
            double sumX = userWeightData.Sum(data => (data.Date - DateTime.MinValue).TotalDays);

            // Calculate the sum of y (weights)
            double sumY = userWeightData.Sum(data => data.Value);

            // Calculate the sum of x^2
            double sumX2 = userWeightData.Sum(data => Math.Pow((data.Date - DateTime.MinValue).TotalDays, 2));

            // Calculate the sum of xy
            double sumXY = userWeightData.Sum(data => (data.Date - DateTime.MinValue).TotalDays * data.Value);

            // Check if there is enough data for linear regression
            double denominator = n * sumX2 - Math.Pow(sumX, 2);
            if (denominator == 0)
            {
                // Return a default value (e.g., -1) when there is insufficient data for prediction
                return -1;
            }

            // Calculate the slope (m) of the regression line
            double slope = (n * sumXY - sumX * sumY) / denominator;

            // Calculate the intercept (b) of the regression line
            double intercept = (sumY - slope * sumX) / n;

            // Predict the next weight
            double nextWeight = slope * ((DateTime.Now - DateTime.MinValue).TotalDays + 1) + intercept;

            return nextWeight;
        }

    }
}
