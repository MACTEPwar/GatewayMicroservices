using AuthMicroservice.Models.DAL;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Infrastructure
{
    public class DbAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Scope> Scopes { get; set; }

        public DbAppContext(DbContextOptions<DbAppContext> options)
            : base(options)
        {
            try
            {
                //  Database.EnsureDeleted();   // create db in first query
                Database.EnsureCreated();   // create db in first query
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e?.InnerException?.Message);
            }
        }
    }
}
