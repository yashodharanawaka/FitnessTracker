using Microsoft.EntityFrameworkCore;
using WorkoutMicroservice.Model;

namespace WorkoutMicroservice.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        
        public DbSet<Workout> Workouts { get; set; }
    }
}
