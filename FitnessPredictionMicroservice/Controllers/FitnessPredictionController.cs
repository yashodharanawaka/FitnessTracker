using Microsoft.AspNetCore.Mvc;
using FitnessPredictionMicroservice.Models;
using FitnessPredictionMicroservice.Services;
using System.Threading.Tasks;

namespace FitnessPredictionMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FitnessPredictionController : ControllerBase
    {
        private readonly IFitnessPredictionService _fitnessPredictionService;

        public FitnessPredictionController(IFitnessPredictionService fitnessPredictionService)
        {
            _fitnessPredictionService = fitnessPredictionService;
        }

        [HttpGet]
        public async Task<ActionResult<FitnessPrediction>> GetFitnessPredictions(int userId)
        {
            return Ok();
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<FitnessPrediction>> GetFitnessPredictionByUserId(int userId)
        {
            var fitnessPrediction = await _fitnessPredictionService.GetFitnessPredictionByUserId(userId);
            if (fitnessPrediction == null)
            {
                return NotFound();
            }

            return Ok(fitnessPrediction);
        }
    }
}
