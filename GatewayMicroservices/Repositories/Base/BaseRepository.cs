using GatewayMicroservices.Infrastructure;
using GatewayMicroservices.Models.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace GatewayMicroservices.Repositories.Base
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : IEntityWithOneKey
    {
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;
        public BaseRepository(IDbContextFactory<DbAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public IQueryable<TModel> GetList()
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                return _dbContext.Route.AsQueryable() as IQueryable<TModel>;
            }
        }
    }

    public interface IBaseRepository<TModel>
    {
        IQueryable<TModel> GetList();
    }
}
