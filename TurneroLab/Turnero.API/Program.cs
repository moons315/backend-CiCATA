using System;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Services.Interfaces;
using Turnero.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar DbContext de EF Core con MySQL
builder.Services.AddDbContext<TurneroDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32))
    )
);

// 2. Registrar servicios de aplicación
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILabService, LabService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

// 3. Registrar controllers, Swagger y API Explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();

// 5. Rutas de controllers
app.MapControllers();

// 6. Endpoint de ejemplo (puedes borrarlo)
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

// Record de ejemplo para el endpoint anterior
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
