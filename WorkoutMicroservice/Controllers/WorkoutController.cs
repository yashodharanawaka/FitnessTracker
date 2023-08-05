using Microsoft.AspNetCore.Mvc;
using WorkoutMicroservice.Model;
using WorkoutMicroservice.Services;

namespace WorkoutTrackingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkoutsByUserId(int userId)
        {
            var workouts = await _workoutService.GetWorkoutsByUserId(userId);
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Workout>> GetWorkoutById(int id)
        {
            var workout = await _workoutService.GetWorkoutById(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }

        [HttpPost]
        public async Task<ActionResult<Workout>> AddWorkout(Workout workout)
        {
            var addedWorkout = await _workoutService.AddWorkout(workout);
            return CreatedAtAction(nameof(GetWorkoutById), new { id = addedWorkout.Id }, addedWorkout);
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<Workout>>> GetWorkoutReportForLastWeek([FromQuery] int userId)
        {
            try
            {
                // Get the workouts for the last week for the specified user id
                var workouts = await _workoutService.GetWorkoutsForLastWeek(userId);

                if (workouts == null || !workouts.Any())
                {
                    return NoContent();
                }

                return Ok(workouts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
