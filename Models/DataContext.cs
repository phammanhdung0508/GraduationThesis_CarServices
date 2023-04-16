#nullable disable
using CarServices.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarServices.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceBooking> ServiceBookings { get; set; }
        public DbSet<ServiceGarage> ServiceGarages { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}