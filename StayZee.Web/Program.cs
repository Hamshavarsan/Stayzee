using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StayZee.Appilication.Interfaces.IRepository;
using StayZee.Application.Interfaces;
using StayZee.Application.Interfaces.IRepository;
using StayZee.Application.Interfaces.Iservices;
using StayZee.Application.Services;
using StayZee.Infrastructure.Data;
using StayZee.Infrastructure.Repository;
using StayZee.Infrastructure.Repostory;
using System.Text;

// using StayZee.Appilication.Common.Models;
// using StayZee.Appilication.Common.Interfaces;
// References removed as EmailSettings is no longer used in Program.cs
// using StayZee.Infrastucture.Services;
// Aliases removed as duplicates are deleted


var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Add DbContext
// ----------------------
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("StayZee.Infrastructure")));

// ----------------------
// Register Repositories
// ----------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();
builder.Services.AddScoped<IHomeApporovalStatusRepository, HomeApporovalStatusRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IRentalRepository, RentalRepository>();
//builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingStatusRepository, BookingStatusRepository>();

// ----------------------
// Register Services
// ----------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<ICloudService, CloudService>();
//builder.Services.AddScoped<IAdminService, AdminService>();
// builder.Services.AddScoped<IEmailService, EmailService>(); // Removed to avoid conflict with manual Singleton registration

// ----------------------
// Email Service
// ----------------------
var emailSection = builder.Configuration.GetSection("EmailSettings");

string smtpHost = emailSection["SmtpHost"];
string smtpPortString = emailSection["SmtpPort"];
int smtpPort = 587; // default
if (!string.IsNullOrEmpty(smtpPortString) && int.TryParse(smtpPortString, out int port))
{
    smtpPort = port;
}

bool enableSsl = bool.Parse(emailSection["EnableSsl"]);
string username = emailSection["Username"];
string password = emailSection["Password"];
string fromAddress = emailSection["FromAddress"];
string fromName = emailSection["FromName"]; // <- define பண்ணவும்

builder.Services.AddSingleton<IEmailService>(new EmailService(
    smtpHost,
    smtpPort,
    enableSsl,
    username,
    password,
    fromAddress,
    fromName // <- Pass பண்ண வேண்டும்
));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
    );
});

// ----------------------
// JWT Authentication
// ----------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

var app = builder.Build();

// ----------------------
// Apply migrations and seed admin
// ----------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        // Apply pending migrations
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Migration warning: {ex.Message}");
        dbContext.Database.EnsureCreated();
    }

    // Seed Admin user if not exists
    if (!dbContext.Users.Any(u => u.Role == "Admin"))
    {
        dbContext.Users.Add(new StayZee.Domain.Entities.User
        {
            Name = "Admin",
            Username = "admin",
            Email = "admin@example.com",
            PhoneNumber = "1234567890",
            NICOrPassport = "A1234567",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Role = "Admin"
        });
        dbContext.SaveChanges();
        Console.WriteLine("Admin user created successfully!");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
