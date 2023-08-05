using CheatMealMicroservice.Models;

namespace CheatMealMicroservice.Services
{
    public interface ICheatMealService
    {
        Task<IEnumerable<CheatMeal>> GetCheatMeals();
        Task<CheatMeal> GetCheatMealById(int id);
        Task<CheatMeal> AddCheatMeal(CheatMeal cheatMeal);
        Task<CheatMeal> UpdateCheatMeal(int id, CheatMeal cheatMeal);
        Task<bool> DeleteCheatMeal(int id);
        Task<IEnumerable<CheatMeal>> GetCheatMealsByUserId(int userId);
        Task<IEnumerable<CheatMeal>> GetCheatMealsForToday(int userId);
    }
}
