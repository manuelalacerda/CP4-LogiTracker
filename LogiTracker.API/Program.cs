using System.Reflection;
using LogiTracker.API.Exceptions;
using LogiTracker.API.Extensions;
using LogiTracker.API.Health;
using LogiTracker.Application.Services;
using LogiTracker.Application.Services.Implementations;
using LogiTracker.Infrastructure;
using LogiTracker.Infrastructure.Persistence;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LogiTracker API",
        Version = "v1",
        Description = "API de gerenciamento logístico do CP3"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

// Banco
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseOracle(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

// Repository genérico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Repository
builder.Services.AddScoped<ICargoRepository, CargoRepository>();

builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();

builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

builder.Services.AddScoped<IDriverRepository, DriverRepository>();

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();

// Serviço de aplicação (usa o repositório genérico para validar dependências)
builder.Services.AddScoped<IDeliveryService, DeliveryService>();

// Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

// Health checks (CP4): self + banco (Oracle, via DbContext do CP2)
builder.Services.AddLogiTrackerHealthChecks();

var app = builder.Build();

// Tratamento global
app.UseExceptionHandler();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// GET /health — único endpoint de health check, não listado no Swagger.
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = HealthCheckResponseWriter.WriteJsonResponse
});

app.Run();