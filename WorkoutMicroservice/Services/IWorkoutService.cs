using System.Collections.Generic;
using System.Threading.Tasks;
using WorkoutMicroservice.Model;

namespace WorkoutMicroservice.Services
{
    public interface IWorkoutService
    {
        Task<IEnumerable<Workout>> GetWorkouts();
        Task<Workout> GetWorkoutById(int id);
        Task<Workout> AddWorkout(Workout workout);
        Task<Workout> UpdateWorkout(int id, Workout workout);
        Task<bool> DeleteWorkout(int id);
        Task<IEnumerable<Workout>> GetWorkoutsByUserId(int userId);
        Task<IEnumerable<Workout>> GetWorkoutsForLastWeek(int userId);
    }
}
