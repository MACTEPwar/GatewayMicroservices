using GatewayMicroservices.Models.DAL;
using Microsoft.EntityFrameworkCore;

namespace GatewayMicroservices.Infrastructure
{
    public class DbAppContext : DbContext
    {
        public DbSet<Models.DAL.Route> Route { get; set; }
        public DbSet<Models.DAL.Setting> Setting{ get; set; }

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
