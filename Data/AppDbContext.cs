using Microsoft.EntityFrameworkCore;
using test_swagger_api.Models;

namespace test_swagger_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
    }
}
