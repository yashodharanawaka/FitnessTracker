using CheatMealMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CheatMealMicroservice.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        
        public DbSet<CheatMeal> CheatMeals { get; set; }
    }
}
