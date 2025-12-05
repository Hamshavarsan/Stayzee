using Microsoft.EntityFrameworkCore;
using StayZee.Domain.Entities;

namespace StayZee.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<HomeApprovalStatus> HomeApprovalStatuses { get; set; }
        public DbSet<HomeDocument> HomeDocuments { get; set; }
        public DbSet<HomeOwner> HomeOwners { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<KYC> KYCs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<OTP> OTPs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<favorite> Favorites { get; set; }
    }
}
