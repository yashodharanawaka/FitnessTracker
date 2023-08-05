using FitnessPredictionMicroservice.Models;
using System.Threading.Tasks;

namespace FitnessPredictionMicroservice.Services
{
    public interface IFitnessPredictionService
    {
        Task<FitnessPrediction> GetFitnessPredictionByUserId(int userId);
    }
}
