using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TransActiva.Context;
using TransActiva.Interface;
using TransActiva.Repository;
using TransActiva.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TransactivaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString(name: "DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString(name: "DefaultConnection"))
    )
);
builder.Services.AddSwaggerGen(options =>
{
    // Definir el esquema de seguridad para Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el formato: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    // Requiere el esquema de seguridad en cada petición
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});
    
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = "car-.*dfr",
            ValidAudience = "acarfa-.@fr4",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secreta-1234567890-firma-segura!"))
        };
    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(name: "Administrador", configurePolicy: 
        policy => policy.RequireRole("Admin"));
    options.AddPolicy(name: "Comprador", configurePolicy: 
        policy => policy.RequireRole("Comprador"));
    options.AddPolicy(name: "Vendor ", configurePolicy: 
        policy => policy.RequireRole("Vendor "));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseSwagger();

    // Swagger UI en la raíz "/"
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté en la raíz
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();