using Microsoft.AspNetCore.Mvc;
using CheatMealMicroservice.Models;
using CheatMealMicroservice.Services;

namespace CheatMealMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheatMealController : ControllerBase
    {
        private readonly ICheatMealService _cheatMealService;

        public CheatMealController(ICheatMealService cheatMealService)
        {
            _cheatMealService = cheatMealService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheatMeal>>> GetCheatMeals()
        {
            var cheatMeals = await _cheatMealService.GetCheatMeals();
            return Ok(cheatMeals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CheatMeal>> GetCheatMealById(int id)
        {
            var cheatMeal = await _cheatMealService.GetCheatMealById(id);
            if (cheatMeal == null)
            {
                return NotFound();
            }
            return Ok(cheatMeal);
        }

        [HttpPost]
        public async Task<ActionResult<CheatMeal>> AddCheatMeal(CheatMeal cheatMeal)
        {
            var addedCheatMeal = await _cheatMealService.AddCheatMeal(cheatMeal);
            return CreatedAtAction(nameof(GetCheatMealById), new { id = addedCheatMeal.Id }, addedCheatMeal);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CheatMeal>> UpdateCheatMeal(int id, CheatMeal cheatMeal)
        {
            var updatedCheatMeal = await _cheatMealService.UpdateCheatMeal(id, cheatMeal);
            if (updatedCheatMeal == null)
            {
                return NotFound();
            }
            return Ok(updatedCheatMeal);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCheatMeal(int id)
        {
            var result = await _cheatMealService.DeleteCheatMeal(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(true);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<CheatMeal>>> GetCheatMealsByUserId(int userId)
        {
            var cheatMeals = await _cheatMealService.GetCheatMealsByUserId(userId);
            return Ok(cheatMeals);
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<CheatMeal>>> GetWorkoutReportForLastWeek([FromQuery] int userId)
        {
            try
            {
                var cheatmeals = await _cheatMealService.GetCheatMealsForToday(userId);

                if (cheatmeals == null || !cheatmeals.Any())
                {
                    return NoContent();
                }

                return Ok(cheatmeals);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
