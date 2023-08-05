using Microsoft.EntityFrameworkCore;
using WorkoutMicroservice.Data;
using WorkoutMicroservice.Model;

namespace WorkoutMicroservice.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly DbContextClass _dbContext;

        public WorkoutService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Workout>> GetWorkouts()
        {
            return await _dbContext.Workouts.ToListAsync();
        }

        public async Task<Workout> GetWorkoutById(int id)
        {
            return await _dbContext.Workouts.FindAsync(id);
        }

        public async Task<Workout> AddWorkout(Workout workout)
        {
            _dbContext.Workouts.Add(workout);
            await _dbContext.SaveChangesAsync();
            return workout;
        }

        public async Task<Workout> UpdateWorkout(int id, Workout workout)
        {
            var existingWorkout = await _dbContext.Workouts.FindAsync(id);
            if (existingWorkout != null)
            {
                // Update properties of existingWorkout with properties from workout
                await _dbContext.SaveChangesAsync();
                return existingWorkout;
            }
            return null;
        }

        public async Task<bool> DeleteWorkout(int id)
        {
            var workout = await _dbContext.Workouts.FindAsync(id);
            if (workout != null)
            {
                _dbContext.Workouts.Remove(workout);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsByUserId(int userId)
        {
            return await _dbContext.Workouts
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Workout>> GetWorkoutsForLastWeek(int userId)
        {
            // Calculate the start date and end date for the last week
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-7);

            // Get all workouts for the specified user within the last week
            var workoutsForLastWeek = await _dbContext.Workouts
                .Where(workout =>
                workout.UserId == userId &&
                                  workout.Date >= startDate &&
                                  workout.Date <= endDate)
                .ToListAsync();

            return workoutsForLastWeek;
        }
    }
}
