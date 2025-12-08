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


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IBookingStatusRepository, BookingStatusRepository>();
builder.Services.AddScoped<IHomeRepository, HomeRepository>();  // <-- IMPORTANT for BookingService


// ----------------------
// Register Services
// ----------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<ICloudService, CloudService>();
builder.Services.AddScoped<IBookingService, BookingService>();

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

builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<IRentalService, RentalService>();
builder.Services.AddScoped<ICloudService, CloudService>();
builder.Services.AddScoped<IAdminService, AdminService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
    );
});

var app = builder.Build();

// Apply pending migrations automatically on startup with error handling
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log the error but continue - the column might already exist
        Console.WriteLine($"Migration warning: {ex.Message}");
        // Try to ensure database is created at minimum
        dbContext.Database.EnsureCreated();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");


app.UseCors("AllowAll");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
