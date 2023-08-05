using Microsoft.AspNetCore.Mvc;
using WeightTrackingMicroservice.Models;
using WeightTrackingMicroservice.Services;

namespace WeightTrackingMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeightController : ControllerBase
    {
        private readonly IWeightService _weightService;

        public WeightController(IWeightService weightService)
        {
            _weightService = weightService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Weight>>> GetWeightsByUserId(int userId)
        {
            var weights = await _weightService.GetWeightsByUserId(userId);
            return Ok(weights);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Weight>> GetWeightById(int id)
        {
            var weight = await _weightService.GetWeightById(id);
            if (weight == null)
            {
                return NotFound();
            }
            return Ok(weight);
        }

        [HttpPost]
        public async Task<ActionResult<Weight>> AddWeight(Weight weight)
        {
            var addedWeight = await _weightService.AddWeight(weight);
            return CreatedAtAction(nameof(GetWeightById), new { id = addedWeight.Id }, addedWeight);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Weight>> UpdateWeight(int id, Weight weight)
        {
            if (id != weight.Id)
            {
                return BadRequest();
            }

            var updatedWeight = await _weightService.UpdateWeight(id, weight);
            if (updatedWeight == null)
            {
                return NotFound();
            }

            return Ok(updatedWeight);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteWeight(int id)
        {
            var result = await _weightService.DeleteWeight(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok(true);
        }

        [HttpGet("nextweight/{userId}")]
        public async Task<ActionResult<double>> CalculateNextWeightForUserAsync(int userId)
        {
            var nextWeight = await _weightService.CalculateNextWeight(userId);

            return Math.Round(nextWeight, 2);
        }
    }
}
