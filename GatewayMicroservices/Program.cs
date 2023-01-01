using GatewayMicroservices.Infrastructure;
using GatewayMicroservices.Srvices;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

app.Run();
