using CheatMealMicroservice.Data;
using CheatMealMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CheatMealMicroservice.Services
{
    public class CheatMealService : ICheatMealService
    {
        private readonly DbContextClass _dbContext;

        public CheatMealService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CheatMeal>> GetCheatMeals()
        {
            return await _dbContext.CheatMeals.ToListAsync();
        }

        public async Task<CheatMeal> GetCheatMealById(int id)
        {
            return await _dbContext.CheatMeals.FindAsync(id);
        }

        public async Task<CheatMeal> AddCheatMeal(CheatMeal cheatMeal)
        {
            _dbContext.CheatMeals.Add(cheatMeal);
            await _dbContext.SaveChangesAsync();
            return cheatMeal;
        }

        public async Task<CheatMeal> UpdateCheatMeal(int id, CheatMeal cheatMeal)
        {
            var existingCheatMeal = await _dbContext.CheatMeals.FindAsync(id);
            if (existingCheatMeal != null)
            {
                await _dbContext.SaveChangesAsync();
                return existingCheatMeal;
            }
            return null;
        }

        public async Task<bool> DeleteCheatMeal(int id)
        {
            var meal = await _dbContext.CheatMeals.FindAsync(id);
            if (meal != null)
            {
                _dbContext.CheatMeals.Remove(meal);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CheatMeal>> GetCheatMealsByUserId(int userId)
        {
            return await _dbContext.CheatMeals
      .Where(w => w.UserId == userId)
      .ToListAsync();
        }

        public async Task<IEnumerable<CheatMeal>> GetCheatMealsForToday(int userId)
        {
            DateTime today = DateTime.Now.Date;

            var cheatMealsForToday = await _dbContext.CheatMeals
                .Where(meal => meal.UserId == userId && meal.Date.Date == today)
                .ToListAsync();

            return cheatMealsForToday;
        }
    }
}
