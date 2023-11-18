using AuthMicroservice.Infrastructure;
using AuthMicroservice.Repositories.Infrostructure;
using AuthMicroservice.Services.Infrostructure;
using LinqKit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
string? connection = configuration.GetConnectionString("Default");

builder.Services.AddDbContextFactory<DbAppContext>(options =>
        options
            .UseNpgsql(connection)
            .WithExpressionExpanding(),
        ServiceLifetime.Scoped
    );

builder.Services.AddAutoMapper(options => options.AddProfile<MappingProfile>());

// Add services to the container.
builder.Services.IncludeRepositories();
builder.Services.IncludeServices();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMyAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
