using AuthMicroservice.Repositories;

namespace AuthMicroservice.Services.Infrostructure
{
    public static class IncludeHelper
    {
        public static IServiceCollection IncludeServices(this IServiceCollection service)
        {
            service.AddScoped<UserService>();
            service.AddScoped<AuthService>();
            return service;
        }
    }
}
