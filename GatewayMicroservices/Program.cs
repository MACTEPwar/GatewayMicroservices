using GatewayMicroservices.Infrastructure;
using GatewayMicroservices.Repositories;
using GatewayMicroservices.Srvices;
using LinqKit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
string? connection = configuration.GetConnectionString("Default");

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddHttpContextAccessor()

    .AddScoped<DestinationService>()
    .AddScoped<RouterService>()
    .AddScoped<SettingService>()

    .AddScoped<SettingRepository>()
    .AddScoped<RouteRepository>()

    .AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), AppDomain.CurrentDomain.GetAssemblies())

    .AddDbContextFactory<DbAppContext>(options =>
        options
            .UseNpgsql(connection)
            .WithExpressionExpanding(),
        ServiceLifetime.Scoped
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run(async (context) => {
    var routerService = app.Services.GetService<RouterService>();
    var content = await routerService.RouteRequest(context.Request);
    await context.Response.WriteAsync(await content.Content.ReadAsStringAsync());
});
