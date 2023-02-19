using AuthMicroservice.Infrastructure;
using AuthMicroservice.Models.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Repositories.Base
{
    public abstract class BaseRepository<TModel> : IDisposable, IBaseRepository<TModel> where TModel : class, IEntityWithOneKey
    {
        private readonly IDbContextFactory<DbAppContext> _dbContextFactory;
        private List<DbAppContext> contextsForDispose = new List<DbAppContext>();
        public BaseRepository(IDbContextFactory<DbAppContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }       

        public IQueryable<TModel> GetList()
        {
            var _dbContext = _dbContextFactory.CreateDbContext();
            DbSet<TModel> table = _dbContext.Set<TModel>();
            var result = table.AsQueryable<TModel>();
            contextsForDispose.Add(_dbContext);
            return result;
        }

        public void Dispose()
        {
            contextsForDispose.ForEach(context => context.Dispose());
        }
    }

    public interface IBaseRepository<TModel>
    {
        IQueryable<TModel> GetList();
    }
}
