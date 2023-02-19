namespace AuthMicroservice.Repositories.Infrostructure
{
    public static class IncludeHelper
    {
        public static IServiceCollection IncludeRepositories(this IServiceCollection service)
        {
            service.AddScoped<UserRepository>();
            service.AddScoped<ScopeRepository>();
            return service;
        }
    }
}
