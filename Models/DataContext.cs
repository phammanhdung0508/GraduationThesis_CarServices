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
        // public DbSet<Payment> Payments { get; set; }
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
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<ProductMediaFile> ProductMediaFiles { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.OneToOneRelationship(modelBuilder);
            this.MultipleCascadePathFix(modelBuilder);
            this.SeedRandomData(modelBuilder);
        }

        private void OneToOneRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Report).WithOne(r => r.Booking)
            .HasForeignKey<Report>(e => e.ReportId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;

            // modelBuilder.Entity<Booking>()
            // .HasOne(b => b.Payment).WithOne(r => r.Booking)
            // .HasForeignKey<Payment>(e => e.PaymentId)
            // //.OnDelete(DeleteBehavior.Cascade)
            // ;
        }

        private void MultipleCascadePathFix(ModelBuilder modelBuilder)
        {

            //path from User to Review
            modelBuilder.Entity<User>()
            .HasMany(u => u.Garages)
            .WithOne(u => u.User)
            .OnDelete(DeleteBehavior.Restrict);

            //path from User to Booking
            modelBuilder.Entity<User>()
            .HasMany(u => u.Cars)
            .WithOne(u => u.User)
            .OnDelete(DeleteBehavior.Restrict);
        }

        private void SeedRandomData(ModelBuilder modelBuilder)
        {
            Faker<User> userFaker;
            Faker<Car> carFaker;
            Faker<Garage> garageFaker;
            Faker<Review> reveiwFaker;
            Faker<Coupon> couponFaker;
            Faker<Report> reportFaker;
            // Faker<Payment> paymentFaker;
            Faker<Booking> bookingFaker;
            Faker<Product> productFaker;
            Faker<ServiceBooking> serviceBookingFaker;
            Faker<ServiceGarage> serviceGarageFaker;
            Faker<Schedule> scheduleFaker;

            Randomizer.Seed = new Random(200);

            EncryptConfiguration encryptConfiguration = new EncryptConfiguration();
            encryptConfiguration.CreatePasswordHash("abc", out byte[] password_hash, out byte[] password_salt);

            modelBuilder.Entity<Role>().HasData(new List<Role>()
            {
                new Role{RoleId=1, RoleName="Admin"},
                new Role{RoleId=2, RoleName="Customer"},
                new Role{RoleId=3, RoleName="Owner"},
                new Role{RoleId=4, RoleName="Mechanic"},
            });

            DateTime now = DateTime.Now;
            modelBuilder.Entity<Category>().HasData(new List<Category>{
                new Category{CategoryId=1, CategoryName="Phụ tùng thay thế", CreatedAt=now, CategoryStatus=1},
                new Category{CategoryId=2, CategoryName="Vật liệu tiêu hao", CreatedAt=now, CategoryStatus=1},
                new Category{CategoryId=3, CategoryName="Công cụ và thiết bị", CreatedAt=now, CategoryStatus=1}
            });

            modelBuilder.Entity<Subcategory>().HasData(new List<Subcategory>{
                //Phụ tùng thay thế, sửa chữa
                new Subcategory{SubcategoryId=1, SubcategoryName="Bộ lọc gió", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=2, SubcategoryName="Bộ lọc dầu", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=3, SubcategoryName="Bộ lọc nhiên liệu", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=4, SubcategoryName="Giảm xóc", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=5, SubcategoryName="Bộ lò xo", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=6, SubcategoryName="Bộ phanh", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=7, SubcategoryName="Cần số", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                new Subcategory{SubcategoryId=8, SubcategoryName="Cầu lái", CreatedAt=now, SubcategoryStatus=1, CategoryId=1},
                //Vật liệu tiêu hao, bảo dưỡng
                new Subcategory{SubcategoryId=9, SubcategoryName="Dầu nhớt", CreatedAt=now, SubcategoryStatus=1, CategoryId=2},
                new Subcategory{SubcategoryId=10, SubcategoryName="Dung dịch làm mát", CreatedAt=now, SubcategoryStatus=1, CategoryId=2},
                new Subcategory{SubcategoryId=11, SubcategoryName="Bình nước rửa kính", CreatedAt=now, SubcategoryStatus=1, CategoryId=2},
                new Subcategory{SubcategoryId=12, SubcategoryName="Nội thất", CreatedAt=now, SubcategoryStatus=1, CategoryId=2},
                new Subcategory{SubcategoryId=13, SubcategoryName="Đèn trước, đèn sau", CreatedAt=now, SubcategoryStatus=1, CategoryId=2},
                new Subcategory{SubcategoryId=14, SubcategoryName="Pin xe", CreatedAt=now, SubcategoryStatus=1, CategoryId=2}
            });

            modelBuilder.Entity<Service>().HasData(new List<Service>{
                //Bảo dưỡng định kỳ
                new Service{ServiceId=1, ServiceName="Thay dầu, bộ lọc", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=2, ServiceName="Kiểm tra hệ thống điện, phanh, treo", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=3, ServiceName="Kiểm tra và thay bình ắc quy, bạc đạn, dây đai", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                //Sửa chữa khẩn cấp
                new Service{ServiceId=4, ServiceName="Thay thế phụ tùng bị hư hỏng", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=5, ServiceName="Sửa chữa động cơ", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=6, ServiceName="Sửa chữa hệ thống điện", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=7, ServiceName="Sửa chữa hệ thống phanh", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                //Nâng cấp xe
                new Service{ServiceId=8, ServiceName="Thay đổi và nâng cấp hệ thống xe", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                new Service{ServiceId=9, ServiceName="Sơn lại xe, cải tạo nội thất, ngoại thất", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
                //Khác
                new Service{ServiceId=10, ServiceName="Rửa xe", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration="10 tiếng", ServiceStatus=1, CreatedAt=now},
            });

            //sunflower-cmd code như cức.

            //Customer
            for (int i = 1; i <= 10; i++)
            {
                userFaker = new Faker<User>()
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
                .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                .RuleFor(u => u.RoleId, f => 4)
                .RuleFor(u => u.UserStatus, UserStatus.Activate)
                .RuleFor(u => u.CreatedAt, now);

                modelBuilder.Entity<User>().HasData(userFaker.Generate());
            }

            //Mechanic
            for (int i = 11; i <= 15; i++)
            {
                userFaker = new Faker<User>()
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
                .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                .RuleFor(u => u.RoleId, f => 3)
                .RuleFor(u => u.UserStatus, UserStatus.Activate)
                .RuleFor(u => u.CreatedAt, now);

                modelBuilder.Entity<User>().HasData(userFaker.Generate());
            }

            //Owner
            for (int i = 16; i <= 30; i++)
            {
                userFaker = new Faker<User>()
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
                .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                .RuleFor(u => u.RoleId, f => 2)
                .RuleFor(u => u.UserStatus, UserStatus.Activate)
                .RuleFor(u => u.CreatedAt, now);

                modelBuilder.Entity<User>().HasData(userFaker.Generate());
            }

            for (int i = 1; i <= 20; i++)
            {
                carFaker = new Faker<Car>()
                .RuleFor(c => c.CarId, i)
                .RuleFor(c => c.CarModel, f => f.Vehicle.Model())
                .RuleFor(c => c.CarBrand, f => f.Vehicle.Manufacturer())
                .RuleFor(c => c.CarLicensePlate, f => f.Random.Replace("##?-###.##"))
                .RuleFor(c => c.CarYear, f => f.Random.Int(1935, 2023))
                .RuleFor(c => c.CarBodyType, f => f.Vehicle.Type())
                .RuleFor(c => c.CarFuelType, f => f.Vehicle.Fuel())
                .RuleFor(c => c.UserId, f => f.Random.Int(1, 10))
                .RuleFor(c => c.CarStatus, 1)
                .RuleFor(c => c.CreatedAt, now);

                modelBuilder.Entity<Car>().HasData(carFaker.Generate());
            }

            for (int i = 1; i <= 25; i++)
            {
                garageFaker = new Faker<Garage>()
                .RuleFor(g => g.GarageId, i)
                .RuleFor(g => g.GarageName, f => f.Name.FirstName() + "Garage")
                .RuleFor(g => g.GarageImage, f => f.Image.PicsumUrl())
                .RuleFor(g => g.GarageContactInformation, f => f.Random.Replace("####.###.###"))
                .RuleFor(g => g.GarageAbout, f => f.Lorem.Paragraph())
                .RuleFor(g => g.GarageAddress, f => f.Address.StreetAddress())
                .RuleFor(g => g.GarageCity, "Ho Chi Minh")
                .RuleFor(g => g.GarageDistrict, f => f.PickRandom(RandomConfiguration.Districts))
                .RuleFor(g => g.GarageWard, (f, g) => { return f.PickRandom(RandomConfiguration.check(g.GarageDistrict));})
                .RuleFor(g => g.Latitude, RandomConfiguration.Location[i].Latitude)
                .RuleFor(g => g.Longitude, RandomConfiguration.Location[i].Longitude)
                .RuleFor(g => g.FromTo, "Monday -> Saturday")
                .RuleFor(g => g.OpenAt, "8AM")
                .RuleFor(g => g.CloseAt, "5PM")
                .RuleFor(g => g.GarageStatus, 1)
                .RuleFor(g => g.UserId, f => f.Random.Int(16, 20))
                .RuleFor(g => g.CreatedAt, now);
                
                modelBuilder.Entity<Garage>().HasData(garageFaker.Generate());
            }

            for (int i = 1; i <= 40; i++)
            {
                reveiwFaker = new Faker<Review>()
                .RuleFor(r => r.ReviewId, i)
                .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                .RuleFor(r => r.Content, f => f.Lorem.Paragraph())
                .RuleFor(r => r.ReviewStatus, 1)
                .RuleFor(r => r.IsApproved, true)
                .RuleFor(r => r.UserId, f => f.Random.Int(1, 10))
                .RuleFor(r => r.GarageId, f => f.Random.Int(1, 10))
                .RuleFor(r => r.CreatedAt, now);

                modelBuilder.Entity<Review>().HasData(reveiwFaker.Generate());
            }

            for (int i = 1; i <= 30; i++)
            {
                couponFaker = new Faker<Coupon>()
                .RuleFor(c => c.CouponId, i)
                .RuleFor(c => c.CouponCode, f => f.Random.Replace("##?###???#"))
                .RuleFor(c => c.CouponType, f => f.PickRandom<CouponType>())
                .RuleFor(c => c.CouponValue, f => f.Random.Float(1, 100))
                .RuleFor(c => c.CouponStartDate, f => f.Date.Recent())
                .RuleFor(c => c.CouponEndDate, f => f.Date.Soon())
                .RuleFor(c => c.CouponMinSpend, f => f.Random.Float(1, 20))
                .RuleFor(c => c.CouponMaxSpend, f => f.Random.Float(60, 100))
                .RuleFor(c => c.NumberOfTimesToUse, f => f.Random.Int(1, 10))
                .RuleFor(c => c.CouponStatus, f => f.PickRandom<CouponStatus>())
                .RuleFor(c => c.GarageId, f => f.Random.Int(1, 25))
                .RuleFor(c => c.CreatedAt, now);

                modelBuilder.Entity<Coupon>().HasData(couponFaker.Generate());
            }

            for (int i = 1; i <= 5; i++)
            {
                reportFaker = new Faker<Report>()
                .RuleFor(r => r.ReportId, i)
                .RuleFor(r => r.Date, f => f.Date.Past())
                .RuleFor(r => r.Notes, f => f.Lorem.Text())
                .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
                .RuleFor(r => r.ReportStatus, f => f.PickRandom<ReportStatus>())
                .RuleFor(r => r.BookingId, f => f.Random.Int(1, 5))
                .RuleFor(r => r.CreatedAt, now);

                modelBuilder.Entity<Report>().HasData(reportFaker.Generate());
            }

            // for (int i = 1; i <= 5; i++)
            // {
            //     paymentFaker = new Faker<Payment>()
            //     .RuleFor(p => p.PaymentId, i)
            //     .RuleFor(p => p.PaymentMethod, f => f.PickRandom(RandomConfiguration.Paymethod))
            //     .RuleFor(p => p.PaymentMessage, f => f.Lorem.Sentence())
            //     .RuleFor(p => p.Currency, "VND")
            //     .RuleFor(p => p.PaymentStatus, f => f.PickRandom<PaymentStatus>())
            //     .RuleFor(p => p.BookingId, f => f.Random.Int(1, 5))
            //     .RuleFor(p => p.CreatedAt, now);

            //     modelBuilder.Entity<Payment>().HasData(paymentFaker.Generate());
            // }

            for (int i = 1; i <= 5; i++)
            {
                bookingFaker = new Faker<Booking>()
                .RuleFor(b => b.BookingId, i)
                .RuleFor(b => b.BookingTime, f => f.Date.Soon())
                .RuleFor(b => b.BookingStatus, f => f.PickRandom<BookingStatus>())
                .RuleFor(b => b.CarId, f => f.Random.Int(1, 15))
                .RuleFor(b => b.TotalCost, f => f.Random.Float(50, 200))
                // .RuleFor(b => b.PaymentId, f => f.Random.Int(1, 5))
                // .RuleFor(b => b.CouponId, f => f.Random.Int(1, 20))
                .RuleFor(b => b.GarageId, f => f.Random.Int(1, 10))
                .RuleFor(b => b.ReportId, f => f.Random.Int(1, 5))
                .RuleFor(b => b.ScheduleId, f => f.Random.Int(1, 5))
                .RuleFor(b => b.CreatedAt, now);

                modelBuilder.Entity<Booking>().HasData(bookingFaker.Generate());
            }

            for (int i = 1; i <= 5; i++){
                scheduleFaker = new Faker<Schedule>()
                .RuleFor(s => s.ScheduleId, i)
                .RuleFor(s => s.BookingTime, now)
                .RuleFor(s => s.WorkDescription, "abc")
                .RuleFor(s => s.ScheduleStatus, 1)
                .RuleFor(s => s.UserId, f => f.Random.Int(11, 15));

                modelBuilder.Entity<Schedule>().HasData(scheduleFaker.Generate());
            }

            for (int i = 1; i <= 20; i++)
            {
                productFaker = new Faker<Product>()
                .RuleFor(p => p.ProductId, i)
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.ProductDetailDescription, f => f.Lorem.Sentence())
                .RuleFor(p => p.ProductPrice, f => f.Random.Float(50, 200))
                .RuleFor(p => p.ProductQuantity, f => f.Random.Int(1, 100))
                .RuleFor(p => p.ProductStatus, 1)
                .RuleFor(p => p.CreatedAt, now)
                .RuleFor(p => p.SubcategoryId, f => f.Random.Int(1, 14))
                .RuleFor(p => p.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<Product>().HasData(productFaker.Generate());
            }

            for (int i = 1; i <= 30; i++)
            {
                serviceBookingFaker = new Faker<ServiceBooking>()
                .RuleFor(s => s.ServiceBookingsId, i)
                .RuleFor(s => s.ProductCost, f => f.Random.Float(50, 200))
                .RuleFor(s => s.ServiceCost, f => f.Random.Float(50, 200))
                .RuleFor(s => s.BookingId, f => f.Random.Int(1, 5))
                .RuleFor(s => s.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<ServiceBooking>().HasData(serviceBookingFaker.Generate());
            }

            for (int i = 1; i <= 30; i++)
            {
                serviceGarageFaker = new Faker<ServiceGarage>()
                .RuleFor(s => s.ServiceGaragesId, i)
                .RuleFor(s => s.LotNumber, f => f.Random.Int(1, 20))
                .RuleFor(s => s.LotStatus, f => f.PickRandom<LotStatus>())
                .RuleFor(s => s.GarageId, f => f.Random.Int(1, 10))
                .RuleFor(s => s.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<ServiceGarage>().HasData(serviceGarageFaker.Generate());
            }
        }
    }
}