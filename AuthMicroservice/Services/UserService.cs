using AuthMicroservice.Models.DAL;
using AuthMicroservice.Repositories;
using AuthMicroservice.Services.Base;

namespace AuthMicroservice.Services
{
    public class UserService : ABaseService<User, UserRepository>
    {
        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
