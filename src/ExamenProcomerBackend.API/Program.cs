using FluentValidation;
using ExamenProcomerBackend.API.Endpoints;
using ExamenProcomerBackend.Application.Interfaces;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Commands;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Handlers;
using ExamenProcomerBackend.Application.CategoriasVehiculo.Validators;
using ExamenProcomerBackend.Application.Vehiculos.Commands;
using ExamenProcomerBackend.Application.Vehiculos.Handlers;
using ExamenProcomerBackend.Application.Vehiculos.Validators;
using ExamenProcomerBackend.Application.Extras.Commands;
using ExamenProcomerBackend.Application.Extras.Handlers;
using ExamenProcomerBackend.Application.Extras.Validators;
using ExamenProcomerBackend.Application.Clientes.Commands;
using ExamenProcomerBackend.Application.Clientes.Handlers;
using ExamenProcomerBackend.Application.Clientes.Validators;
using ExamenProcomerBackend.Infrastructure.Data;
using ExamenProcomerBackend.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ Minimal API (NO Controllers)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new() { Title = builder.Configuration["Swagger:Title"] ?? "ExamenProcomerBackend API", Version = "v1" });
    
    // Configurar JWT en Swagger
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese 'Bearer' [espacio] y luego su token JWT.\n\nEjemplo: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...\""
    });

    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuración JWT
var jwtSecret = builder.Configuration["Jwt:Secret"] ?? throw new InvalidOperationException("Jwt:Secret no configurado");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer no configurado");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience no configurado");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

builder.Services.AddAuthorization();

var cs = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Missing ConnectionStrings:Default");

// Infrastructure - Repositories
builder.Services.AddSingleton(new DapperContext(cs));

// CategoriaVehiculo Repositories
builder.Services.AddScoped<ICategoriaVehiculoCommandRepository, CategoriaVehiculoCommandRepository>();
builder.Services.AddScoped<ICategoriaVehiculoQueryRepository, CategoriaVehiculoQueryRepository>();

// Vehiculo Repositories
builder.Services.AddScoped<IVehiculoCommandRepository, VehiculoCommandRepository>();
builder.Services.AddScoped<IVehiculoQueryRepository, VehiculoQueryRepository>();

// Extra Repositories
builder.Services.AddScoped<IExtraCommandRepository, ExtraCommandRepository>();
builder.Services.AddScoped<IExtraQueryRepository, ExtraQueryRepository>();

// Cliente Repositories
builder.Services.AddScoped<IClienteCommandRepository, ClienteCommandRepository>();
builder.Services.AddScoped<IClienteQueryRepository, ClienteQueryRepository>();

// Validators - CategoriaVehiculo
builder.Services.AddScoped<IValidator<CrearCategoriaVehiculoCommand>, CrearCategoriaVehiculoCommandValidator>();
builder.Services.AddScoped<IValidator<ActualizarCategoriaVehiculoCommand>, ActualizarCategoriaVehiculoCommandValidator>();

// Validators - Vehiculo
builder.Services.AddScoped<IValidator<CrearVehiculoCommand>, CrearVehiculoCommandValidator>();
builder.Services.AddScoped<IValidator<ActualizarVehiculoCommand>, ActualizarVehiculoCommandValidator>();

// Validators - Extra
builder.Services.AddScoped<IValidator<CrearExtraCommand>, CrearExtraCommandValidator>();
builder.Services.AddScoped<IValidator<ActualizarExtraCommand>, ActualizarExtraCommandValidator>();

// Validators - Cliente
builder.Services.AddScoped<IValidator<CrearClienteCommand>, CrearClienteCommandValidator>();
builder.Services.AddScoped<IValidator<ActualizarClienteCommand>, ActualizarClienteCommandValidator>();

// Handlers - CategoriaVehiculo
builder.Services.AddScoped<CategoriaVehiculoCommandHandler>();
builder.Services.AddScoped<CategoriaVehiculoQueryHandler>();

// Handlers - Vehiculo
builder.Services.AddScoped<VehiculoCommandHandler>();
builder.Services.AddScoped<VehiculoQueryHandler>();

// Handlers - Extra
builder.Services.AddScoped<ExtraCommandHandler>();
builder.Services.AddScoped<ExtraQueryHandler>();

// Handlers - Cliente
builder.Services.AddScoped<ClienteCommandHandler>();
builder.Services.AddScoped<ClienteQueryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// ⚠️ IMPORTANTE: UseAuthentication ANTES de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Ok(new { status = "ok", solution = "MS_Catalogo", utc = DateTime.UtcNow }));

// Mapear endpoints de autenticación (login)
app.MapAuthEndpoints();

// Mapear endpoints de Catálogo
app.MapCategoriasVehiculoEndpoints();
app.MapVehiculosEndpoints();
app.MapExtrasEndpoints();
app.MapClientesEndpoints();

app.Run();

public partial class Program { }
