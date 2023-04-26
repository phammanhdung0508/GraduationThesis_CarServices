#nullable disable
using Bogus;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Randomly;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Models
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
            this.OneToOneRelationship(modelBuilder);
            this.SeedRandomData(modelBuilder);
        }

        private void OneToOneRelationship(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Report).WithOne(r => r.Booking)
            .HasForeignKey<Report>(e => e.ReportId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Payment).WithOne(r => r.Booking)
            .HasForeignKey<Payment>(e => e.PaymentId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;
        }

        private void SeedRandomData(ModelBuilder modelBuilder)
        {
            Faker<User> faker;

            Randomizer.Seed = new Random(200);

            EncryptConfiguration encryptConfiguration = new EncryptConfiguration();
            encryptConfiguration.CreatePasswordHash("abc", out byte[] password_hash, out byte[] password_salt);

            List<Role> listRole = new List<Role>()
            {
                new Role{RoleId=1, RoleName="Admin"},
                new Role{RoleId=2, RoleName="Customer"},
                new Role{RoleId=3, RoleName="Owner"},
                new Role{RoleId=4, RoleName="Mechanic"},
            };
            modelBuilder.Entity<Role>().HasData(listRole);

            for (int i = 1; i <= 5; i++)
            {
                faker = new Faker<User>()
                .RuleFor(u => u.UserId, i)
                .RuleFor(u => u.UserFirstName, f => f.Name.FirstName())
                .RuleFor(u => u.UserLastName, f => f.Name.LastName())
                .RuleFor(u => u.UserEmail, (f, u) => f.Internet.Email(u.UserFirstName, u.UserLastName))
                .RuleFor(u => u.PasswordHash, password_hash)
                .RuleFor(u => u.PasswordSalt, password_salt)
                .RuleFor(u => u.UserAddress, f => f.Address.StreetAddress())
                .RuleFor(u => u.UserCity, "Ho Chi Minh")
                .RuleFor(u => u.UserDistrict, f => f.PickRandom(RandomConfiguration.Districts))
                .RuleFor(u => u.UserWard, (f, u) => { return f.PickRandom(RandomConfiguration.check(u.UserDistrict)); })
                .RuleFor(u => u.UserPhone, f => f.Phone.PhoneNumberFormat())
                .RuleFor(u => u.UserGender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.UserDateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(u => u.UserImage, f => f.Internet.Avatar())
                .RuleFor(u => u.UserBio, f => f.Lorem.Paragraphs())
                .RuleFor(u => u.RoleId, f => f.Random.Int(1, 4));

                modelBuilder.Entity<User>().HasData(faker.Generate());
            }
        }
    }
}