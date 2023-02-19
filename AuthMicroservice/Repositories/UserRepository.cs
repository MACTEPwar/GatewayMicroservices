using AuthMicroservice.Infrastructure;
using AuthMicroservice.Models.DAL;
using AuthMicroservice.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IDbContextFactory<DbAppContext> dbContextFactory) : base(dbContextFactory) { }
    }
}
