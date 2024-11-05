using CO2EmissionsAPI.Interfaces;
using CO2EmissionsAPI.Repositories;
using CO2EmissionsAPI.Models;
using CO2EmissionsAPI.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add appSettings JSON configuration files for different environments
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the DI container
// This setup allows controllers to receive dependencies via DI

// Add CORS policy for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// register support for controllers, CORS, API explorers, etc. 
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// add swagger service for API documentation
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "x-api-key", // The header name where the API key is sent
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "API Key needed to access the endpoints. Add 'x-api-key' header with your API key."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});

// register the EFCore DbContext WorldBankEmissionsContext while setting the SQLite connection string
var connectionString = builder.Configuration.GetConnectionString("EmissionsDb");
builder.Services.AddDbContext<WorldBankEmissionsContext>(options => options.UseSqlite(connectionString));

// register the WorldBankEmissionsRepository
builder.Services.AddScoped<IWorldBankEmissionsRepository, WorldBankEmissionsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS policy
app.UseCors("AllowReactApp");

// Enable middleware for api key
app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

