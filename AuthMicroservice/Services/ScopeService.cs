using AuthMicroservice.Models.DAL;
using AuthMicroservice.Repositories;
using AuthMicroservice.Services.Base;

namespace AuthMicroservice.Services
{
    public class ScopeService : ABaseService<Scope, ScopeRepository>
    {
        public ScopeService(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
    }
}
