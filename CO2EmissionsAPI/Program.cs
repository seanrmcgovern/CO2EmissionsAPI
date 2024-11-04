using CO2EmissionsAPI.Interfaces;
using CO2EmissionsAPI.Repositories;
using CO2EmissionsAPI.Models;
using CO2EmissionsAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add appSettings JSON configuration files for different environments
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add services to the DI container
// This setup allows controllers to receive dependencies via DI

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

// register the EFCore DbContext WorldBankEmissionsContext
builder.Services.AddDbContext<WorldBankEmissionsContext>();

// register the WorldBankEmissionsRepository
builder.Services.AddScoped<IWorldBankEmissionsRepository, WorldBankEmissionsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

