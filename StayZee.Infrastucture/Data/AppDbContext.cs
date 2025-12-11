using Microsoft.EntityFrameworkCore;
using StayZee.Domain.Entities;
using System;
using System.Linq;

namespace StayZee.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<HomeOwner> HomeOwners { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeApprovalStatus> HomeApprovalStatuses { get; set; }
        public DbSet<HomeDocument> HomeDocuments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<KYC> KYCs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<favorite> Favorites { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BookingSharedCustomer> BookingSharedCustomers { get; set; }
        public DbSet<AdminIncome> AdminIncomes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set decimal precision globally
            foreach (var prop in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                prop.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(modelBuilder);

            // ----------------- Booking -----------------
            

            // ----------------- KYC -----------------
            modelBuilder.Entity<KYC>()
                .HasOne(k => k.Customer)
                .WithMany(c => c.KYCUploads)
                .HasForeignKey(k => k.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // ----------------- HomeDocument -----------------
            modelBuilder.Entity<HomeDocument>()
                .HasOne(d => d.home)
                .WithMany(h => h.Documents)
                .HasForeignKey(d => d.HomeId)
                .OnDelete(DeleteBehavior.Cascade);

            //// ----------------- Payment -----------------
            //modelBuilder.Entity<Payment>()
            //    .HasOne(p => p.Booking)
            //    .WithOne(b => b.Payment)
            //    .HasForeignKey<Payment>(p => p.BookingId);

            //modelBuilder.Entity<Payment>()
            //    .Property(p => p.Amount)
            //    .HasPrecision(18, 2);

            //// ----------------- Home -----------------
            //modelBuilder.Entity<Home>()
            //    .Property(h => h.RatePerDay)
            //    .HasPrecision(18, 2);

            //modelBuilder.Entity<Home>()
            //    .HasOne(h => h.HomeApprovalStatus)
            //    .WithMany(s => s.Homes)
            //    .HasForeignKey(h => h.HomeApprovalStatusId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //// ----------------- Property -----------------
            //modelBuilder.Entity<Property>()
            //    .HasIndex(p => p.Id)
            //    .IsUnique();

            //// ----------------- BookingSharedCustomer -----------------
            //modelBuilder.Entity<BookingSharedCustomer>(bsc =>
            //{
            //    bsc.HasKey(x => x.Id);

            //    bsc.HasOne(x => x.Booking)
            //       .WithMany(b => b.SharedCustomers)
            //       .HasForeignKey(x => x.BookingId)
            //       .OnDelete(DeleteBehavior.Cascade);

            //    bsc.HasOne(x => x.Customer)
            //       .WithMany(c => c.SharedBookings)
            //       .HasForeignKey(x => x.CustomerId)
            //       .OnDelete(DeleteBehavior.Cascade);

            //    // Unique index to prevent duplicates
            //    bsc.HasIndex(x => new { x.BookingId, x.CustomerId }).IsUnique();
            //});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=YOUR_SERVER;Database=StayZeeDb;Trusted_Connection=True;",
                    b => b.MigrationsAssembly("StayZee.Infrastructure")
                );
            }
        }
    }
}
