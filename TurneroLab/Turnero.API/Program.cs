using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Turnero.API.Settings;
using Turnero.Data.Context;
using Turnero.Infrastructure.Email;
using Turnero.Infrastructure.Background;
using Turnero.Services.Settings;
using Turnero.Services.Interfaces;
using Turnero.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar configuración de appsettings.json
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// 2. Registrar DbContext de EF Core con MySQL
builder.Services.AddDbContext<TurneroDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32))
    )
);

<<<<<<< HEAD
// 2. Registrar servicios de aplicaciÃ³n
=======
// 3. Configuración de JWT
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings")
);

// 4. Configuración de Email
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("Email")
);

// 5. Registrar servicios de aplicación
>>>>>>> 3a9683bcb1b3cf56b4f4c4f990c948d6ded88282
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILabService, LabService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IReportService, ReportService>();

<<<<<<< HEAD
// --- POLÃTICA DE CORS ---
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 3. Registrar controllers, Swagger y API Explorer
=======
// 6. Registrar EmailService y BackgroundService
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddHostedService<ReservationBackgroundService>();

// 7. Configurar autenticación JWT
var jwtConfig = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtConfig.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtConfig.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// 8. Registrar autorización
builder.Services.AddAuthorization();

// 9. Registrar controllers, Swagger y CORS
>>>>>>> 3a9683bcb1b3cf56b4f4c4f990c948d6ded88282
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(policy =>
    policy.AddDefaultPolicy(cors =>
        cors.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    )
);

var app = builder.Build();

// 10. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

<<<<<<< HEAD
// --- USAR CORS ---
app.UseCors();

// app.UseHttpsRedirection();
=======
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
>>>>>>> 3a9683bcb1b3cf56b4f4c4f990c948d6ded88282
app.UseAuthorization();

// 11. Mapear controllers
app.MapControllers();

app.Run();
<<<<<<< HEAD

// Record de ejemplo para el endpoint anterior
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
=======
>>>>>>> 3a9683bcb1b3cf56b4f4c4f990c948d6ded88282
