using AuthMicroservice.Infrastructure;
using AuthMicroservice.Models.DAL;
using AuthMicroservice.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Repositories
{
    public class ScopeRepository : BaseRepository<Scope>
    {
        public ScopeRepository(IDbContextFactory<DbAppContext> dbContextFactory) : base(dbContextFactory) { }
    }
}
