using CO2EmissionsAPI.Interfaces;
using CO2EmissionsAPI.Repositories;
using CO2EmissionsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
// This setup allows controllers to receive dependencies via DI

// register support for controllers, CORS, API explorers, etc. 
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// add swagger service for API documentation
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

