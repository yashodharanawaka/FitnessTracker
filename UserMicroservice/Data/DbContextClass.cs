using Microsoft.EntityFrameworkCore;
using UserMicroservice.Model;

namespace UserMicroservice.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        // DbSet and other configurations...
        
        // DbSet for User entity
        public DbSet<User> Users { get; set; }
    }
}
