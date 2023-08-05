using Microsoft.EntityFrameworkCore;
using WeightTrackingMicroservice.Models;

namespace WeightTrackingMicroservice.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        
        public DbSet<Weight> Weights { get; set; }
    }
}
