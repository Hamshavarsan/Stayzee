using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StayZee.Appilication.Interfaces.IRepository;
using StayZee.Application.Interfaces;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Application.Services;
using StayZee.Infrastructure.Data;
using StayZee.Infrastructure.Repositories;
using StayZee.Infrastructure.Repository;
using StayZee.Infrastructure.Repostory;
using System.Text;

// Removed invalid using directive
// using StayZee.Appilication.Interfaces.IRepository

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
b => b.MigrationsAssembly("StayZee.Infrastructure")));

// Register Services & Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IHomeApporovalStatusRepository, HomeApporovalStatusRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<ICloudService, CloudService>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();