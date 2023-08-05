using System.Collections.Generic;
using System.Threading.Tasks;
using WeightTrackingMicroservice.Models;

namespace WeightTrackingMicroservice.Services
{
    public interface IWeightService
    {
        Task<IEnumerable<Weight>> GetWeightsByUserId(int userId);
        Task<Weight> GetWeightById(int id);
        Task<Weight> AddWeight(Weight weight);
        Task<Weight> UpdateWeight(int id, Weight weight);
        Task<bool> DeleteWeight(int id);
        Task<double> CalculateNextWeight(int userId);
    }
}
