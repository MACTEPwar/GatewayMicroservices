using GatewayMicroservices.Infrastructure;
using GatewayMicroservices.Models.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace GatewayMicroservices.Repositories.Base
{
    public abstract class BaseRepository<TModel> : IDisposable, IBaseRepository<TModel> where TModel : IEntityWithOneKey
    {
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;
        public DbAppContext _dbContext;
        public BaseRepository(IDbContextFactory<DbAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            CreateContext();
        }       

        public BaseRepository<TModel> CreateContext()
        {
            _dbContext = _dbContextFactory.CreateDbContext();
            return this;
        }

        public IQueryable<TModel> GetList()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            return _dbContext.Route.AsQueryable() as IQueryable<TModel>;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

    public interface IBaseRepository<TModel>
    {
        IQueryable<TModel> GetList();
    }
}
