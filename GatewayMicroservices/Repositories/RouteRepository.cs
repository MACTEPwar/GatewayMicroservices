using GatewayMicroservices.Infrastructure;
using GatewayMicroservices.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GatewayMicroservices.Repositories
{
    public class RouteRepository : BaseRepository<Models.DAL.Route>
    {
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;
        public RouteRepository(IDbContextFactory<DbAppContext> dbContextFactory) : base(dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
    }
}
