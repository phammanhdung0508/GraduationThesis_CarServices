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
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceBooking> ServiceBookings { get; set; }
        public DbSet<ServiceGarage> ServiceGarages { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MediaFile> MediaFiles { get; set; }
        public DbSet<ProductMediaFile> ProductMediaFiles { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<WorkingSchedule> WorkingSchedules { get; set; }
        public DbSet<Lot> Lots { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.OneToOneRelationship(modelBuilder);
            this.MultipleCascadePathFix(modelBuilder);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Randomizer.Seed = new Random(200);
            this.SeedRoleData(modelBuilder);
            this.SeedCategoryData(modelBuilder);
            this.SeedSubcategoryData(modelBuilder);
            this.SeedServiceData(modelBuilder);
            this.SeedRandomProductData(modelBuilder);
            this.SeedRandomUserData(modelBuilder);
            this.SeedRandomWorkingScheduleData(modelBuilder);
            this.SeedRandomCarData(modelBuilder);
            this.SeedRandomGarageData(modelBuilder);
            this.SeedRandomReviewData(modelBuilder);
            this.SeedRandomCouponData(modelBuilder);
            this.SeedRandomBookingData(modelBuilder);
            this.SeedRandomReportData(modelBuilder);
            watch.Stop();
            Console.WriteLine($"Total run time: {watch.ElapsedMilliseconds}");
        }

        private void OneToOneRelationship(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
            .HasOne(b => b.Report).WithOne(r => r.Booking)
            .HasForeignKey<Report>(e => e.ReportId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;

            modelBuilder.Entity<User>()
            .HasOne(b => b.Customer).WithOne(r => r.User)
            .HasForeignKey<Customer>(e => e.CustomerId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;


            modelBuilder.Entity<User>()
            .HasOne(b => b.Mechanic).WithOne(r => r.User)
            .HasForeignKey<Mechanic>(e => e.MechanicId)
            //.OnDelete(DeleteBehavior.Cascade)
            ;
        }

        private void MultipleCascadePathFix(ModelBuilder modelBuilder)
        {

            //path from User to Review
            modelBuilder.Entity<User>()
            .HasMany(u => u.Garages)
            .WithOne(u => u.User)
            .OnDelete(DeleteBehavior.Restrict);
        }

        private readonly DateTime now = DateTime.Now;
        private void SeedRoleData(ModelBuilder modelBuilder)
        {
            var list = new List<Role>()
            {
                new Role{RoleId=1, RoleName="Customer"},
                new Role{RoleId=2, RoleName="Manager"},
                new Role{RoleId=3, RoleName="Mechanic"},
                new Role{RoleId=4, RoleName="Admin"},
            };
            modelBuilder.Entity<Role>().HasData(list);
        }

        private void SeedCategoryData(ModelBuilder modelBuilder)
        {
            var list = new List<Category>{
                new Category{CategoryId=1, CategoryName="Phụ tùng thay thế", CreatedAt=now, CategoryStatus=Status.Activate},
                new Category{CategoryId=2, CategoryName="Vật liệu tiêu hao", CreatedAt=now, CategoryStatus=Status.Activate},
                new Category{CategoryId=3, CategoryName="Công cụ và thiết bị", CreatedAt=now, CategoryStatus=Status.Activate}
            };
            modelBuilder.Entity<Category>().HasData(list);
        }

        private void SeedSubcategoryData(ModelBuilder modelBuilder)
        {
            var list = new List<Subcategory>{
                //Phụ tùng thay thế, sửa chữa
                new Subcategory{SubcategoryId=1, SubcategoryName="Bộ lọc gió", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=2, SubcategoryName="Bộ lọc dầu", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=3, SubcategoryName="Bộ lọc nhiên liệu", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=4, SubcategoryName="Giảm xóc", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=5, SubcategoryName="Bộ lò xo", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=6, SubcategoryName="Bộ phanh", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=7, SubcategoryName="Cần số", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},
                new Subcategory{SubcategoryId=8, SubcategoryName="Cầu lái", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=1},

                //Vật liệu tiêu hao, bảo dưỡng
                new Subcategory{SubcategoryId=9, SubcategoryName="Dầu nhớt", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2},
                new Subcategory{SubcategoryId=10, SubcategoryName="Dung dịch làm mát", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2},
                new Subcategory{SubcategoryId=11, SubcategoryName="Bình nước rửa kính", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2},
                new Subcategory{SubcategoryId=12, SubcategoryName="Nội thất", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2},
                new Subcategory{SubcategoryId=13, SubcategoryName="Đèn trước, đèn sau", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2},
                new Subcategory{SubcategoryId=14, SubcategoryName="Pin xe", CreatedAt=now, SubcategoryStatus=Status.Activate, CategoryId=2}
            };
            modelBuilder.Entity<Subcategory>().HasData(list);
        }

        private void SeedServiceData(ModelBuilder modelBuilder)
        {
            var list = new List<Service>{
                //Bảo dưỡng định kỳ
                new Service{ServiceId=1, ServiceName="Thay dầu, bộ lọc", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=5, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=2, ServiceName="Kiểm tra hệ thống điện, phanh, treo", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=2, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=3, ServiceName="Kiểm tra và thay bình ắc quy, bạc đạn, dây đai", ServiceImage="",
                ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                ServiceDuration=3, ServiceStatus=Status.Activate, CreatedAt=now},

                //Sửa chữa khẩn cấp
                new Service{ServiceId=4, ServiceName="Thay thế phụ tùng bị hư hỏng", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=4, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=5, ServiceName="Sửa chữa động cơ", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=5, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=6, ServiceName="Sửa chữa hệ thống điện", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=6, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=7, ServiceName="Sửa chữa hệ thống phanh", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=7, ServiceStatus=Status.Activate, CreatedAt=now},

                //Nâng cấp xe
                new Service{ServiceId=8, ServiceName="Thay đổi và nâng cấp hệ thống xe", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=10, ServiceStatus=Status.Activate, CreatedAt=now},
                new Service{ServiceId=9, ServiceName="Sơn lại xe, cải tạo nội thất, ngoại thất", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=12, ServiceStatus=Status.Activate, CreatedAt=now},

                //Khác
                new Service{ServiceId=10, ServiceName="Rửa xe", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text of the printing and typesetting industry.", ServicePrice=100,
                    ServiceDuration=11, ServiceStatus=Status.Activate, CreatedAt=now},
            };
            modelBuilder.Entity<Service>().HasData(list);
        }

        private void SeedRandomProductData(ModelBuilder modelBuilder)
        {
            var productFaker = new Faker<Product>();

            for (int i = 1; i <= 30; i++)
            {
                productFaker.RuleFor(p => p.ProductId, i)
                    .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                    .RuleFor(p => p.ProductDetailDescription, f => f.Lorem.Sentence())
                    .RuleFor(p => p.ProductPrice, f => f.Random.Float(50, 200))
                    .RuleFor(p => p.ProductQuantity, f => f.Random.Int(1, 100))
                    .RuleFor(p => p.ProductStatus, Status.Activate)
                    .RuleFor(p => p.CreatedAt, now)
                    .RuleFor(p => p.SubcategoryId, f => f.Random.Int(1, 14))
                    .RuleFor(p => p.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<Product>().HasData(productFaker.Generate());
            }
        }

        private void SeedRandomUserData(ModelBuilder modelBuilder)
        {
            var customerFaker = new Faker<Customer>();
            var mechanicFaker = new Faker<Mechanic>();
            var userFaker = new Faker<User>();

            var encryptConfiguration = new EncryptConfiguration();
            encryptConfiguration.CreatePasswordHash("abc", out byte[] password_hash, out byte[] password_salt);

            int m = 0;
            for (int i = 1; i <= 50; i++)
            {
                switch (i)
                {
                    case <= 20:
                        customerFaker.RuleFor(c => c.CustomerId, i)
                            .RuleFor(c => c.CustomerAddress, f => f.Address.StreetAddress())
                            .RuleFor(c => c.CustomerDistrict, f => f.PickRandom(RandomConfiguration.Districts))
                            .RuleFor(c => c.CustomerWard, (f, g) => { return f.PickRandom(RandomConfiguration.check(g.CustomerDistrict)); })
                            .RuleFor(c => c.CustomerCity, "Hồ Chí Minh");

                        modelBuilder.Entity<Customer>().HasData(customerFaker.Generate());
                        break;
                    case > 30 and <= 49:
                        mechanicFaker.RuleFor(m => m.MechanicId, ++m)
                            .RuleFor(m => m.TotalWorkingHours, f => f.Random.Int(0, 15))
                            .RuleFor(m => m.Specialities, f => f.Lorem.Lines());

                        modelBuilder.Entity<Mechanic>().HasData(mechanicFaker.Generate());
                        break;
                }

                userFaker.RuleFor(u => u.UserId, i)
                    .RuleFor(u => u.UserFirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.UserLastName, f => f.Name.LastName())
                    .RuleFor(u => u.UserEmail, (f, u) => f.Internet.Email(u.UserFirstName, u.UserLastName))
                    .RuleFor(u => u.PasswordHash, password_hash)
                    .RuleFor(u => u.PasswordSalt, password_salt)
                    .RuleFor(u => u.UserImage, f => f.Internet.Avatar())
                    .RuleFor(u => u.UserPhone, f => f.Phone.PhoneNumberFormat())
                    .RuleFor(u => u.UserGender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.UserDateOfBirth, f => f.Person.DateOfBirth)
                    .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                    .RuleFor(u => u.UserStatus, Status.Activate)
                    .RuleFor(u => u.CreatedAt, now)
                    .RuleFor(u => u.RoleId, f =>
                    {
                        switch (i)
                        {
                            case <= 20:
                                return 1;
                            case > 20 and <= 30:
                                return 2;
                            case > 30 and <= 49:
                                return 3;
                            case 50:
                                return 4;
                            default: return 1;
                        }
                    });

                modelBuilder.Entity<User>().HasData(userFaker.Generate());
            }
        }

        private void SeedRandomWorkingScheduleData(ModelBuilder modelBuilder)
        {
            var workingScheduleFaker = new Faker<WorkingSchedule>();

            for (int i = 1; i <= 30; i++)
            {
                workingScheduleFaker.RuleFor(w => w.WorkingScheduleId, i)
                    .RuleFor(w => w.StartTime, "7AM")
                    .RuleFor(w => w.EndTime, "5PM")
                    .RuleFor(w => w.DaysOfTheWeek, "Monday")
                    .RuleFor(w => w.Description, f => f.Lorem.Lines())
                    .RuleFor(w => w.WorkingScheduleStatus, f => f.PickRandom<WorkingScheduleStatus>())
                    .RuleFor(w => w.GarageId, f => f.Random.Int(1, 25))
                    .RuleFor(s => s.MechanicId, f => f.Random.Int(1, 19));

                modelBuilder.Entity<WorkingSchedule>().HasData(workingScheduleFaker.Generate());
            }
        }

        private void SeedRandomCarData(ModelBuilder modelBuilder)
        {
            var carFaker = new Faker<Car>();

            for (int i = 1; i <= 20; i++)
            {
                carFaker.RuleFor(c => c.CarId, i)
                    .RuleFor(c => c.CarColor, f => f.Commerce.Color())
                    .RuleFor(c => c.CarModel, f => f.Vehicle.Model())
                    .RuleFor(c => c.CarBrand, f => f.Vehicle.Manufacturer())
                    .RuleFor(c => c.CarLicensePlate, f => f.Random.Replace("##?-###.##"))
                    .RuleFor(c => c.CarYear, f => f.Random.Int(1935, 2023))
                    .RuleFor(c => c.CarBodyType, f => f.Vehicle.Type())
                    .RuleFor(c => c.CarFuelType, f => f.Vehicle.Fuel())
                    .RuleFor(c => c.CarStatus, Status.Activate)
                    .RuleFor(c => c.CreatedAt, now)
                    .RuleFor(c => c.CustomerId, f => f.Random.Int(1, 20));

                modelBuilder.Entity<Car>().HasData(carFaker.Generate());
            }
        }

        private void SeedRandomGarageData(ModelBuilder modelBuilder)
        {
            var garageFaker = new Faker<Garage>();
            var serviceGarageFaker = new Faker<ServiceGarage>();
            var lotFaker = new Faker<Lot>();

            for (int i = 1; i <= 25; i++)
            {
                garageFaker.RuleFor(g => g.GarageId, i)
                    .RuleFor(g => g.GarageName, f => f.Name.FirstName() + " Garage")
                    .RuleFor(g => g.GarageAbout, f => f.Lorem.Paragraph())
                    .RuleFor(g => g.GarageImage, f => f.Image.PicsumUrl())
                    .RuleFor(g => g.GarageContactInformation, f => f.Random.Replace("####.###.###"))
                    .RuleFor(g => g.FromTo, "Monday to Saturday")
                    .RuleFor(g => g.OpenAt, "08:00 AM")
                    .RuleFor(g => g.CloseAt, "05:00 PM")
                    .RuleFor(g => g.GarageAddress, f => f.Address.StreetAddress())
                    .RuleFor(g => g.GarageCity, "Ho Chi Minh")
                    .RuleFor(g => g.GarageDistrict, f => f.PickRandom(RandomConfiguration.Districts))
                    .RuleFor(g => g.GarageWard, (f, g) => { return f.PickRandom(RandomConfiguration.check(g.GarageDistrict)); })
                    .RuleFor(g => g.GarageLatitude, RandomConfiguration.Location[i].Latitude)
                    .RuleFor(g => g.GarageLongitude, RandomConfiguration.Location[i].Longitude)
                    .RuleFor(g => g.GarageStatus, Status.Activate)
                    .RuleFor(g => g.CreatedAt, now)
                    .RuleFor(g => g.UserId, f => f.Random.Int(21, 30));


                modelBuilder.Entity<Garage>().HasData(garageFaker.Generate());

            }

            for (int i = 1; i <= 70; i++)
            {
                serviceGarageFaker.RuleFor(s => s.ServiceGaragesId, i)
                    .RuleFor(s => s.GarageId, f => f.Random.Int(1, 25))
                    .RuleFor(s => s.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<ServiceGarage>().HasData(serviceGarageFaker.Generate());
            }

            for (int i = 1; i <= 100; i++)
            {
                lotFaker.RuleFor(l => l.LotId, i)
                    .RuleFor(l => l.LotNumber, f => f.Random.Replace("#?"))
                    .RuleFor(l => l.LotStatus, f => f.PickRandom<LotStatus>())
                    .RuleFor(l => l.GarageId, f => f.Random.Int(1, 25));

                modelBuilder.Entity<Lot>().HasData(lotFaker.Generate());
            }
        }

        private void SeedRandomReviewData(ModelBuilder modelBuilder)
        {
            var reveiwFaker = new Faker<Review>();

            for (int i = 1; i <= 40; i++)
            {
                reveiwFaker.RuleFor(r => r.ReviewId, i)
                    .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
                    .RuleFor(r => r.Content, f => f.Lorem.Paragraph())
                    .RuleFor(r => r.ReviewStatus, Status.Activate)
                    .RuleFor(r => r.CustomerId, f => f.Random.Int(1, 20))
                    .RuleFor(r => r.GarageId, f => f.Random.Int(1, 25))
                    .RuleFor(r => r.CreatedAt, now);

                modelBuilder.Entity<Review>().HasData(reveiwFaker.Generate());
            }
        }

        private void SeedRandomCouponData(ModelBuilder modelBuilder)
        {
            var couponFaker = new Faker<Coupon>();

            for (int i = 1; i <= 30; i++)
            {
                couponFaker.RuleFor(c => c.CouponId, i)
                    .RuleFor(c => c.CouponCode, f => f.Random.Replace("##?###???#"))
                    .RuleFor(c => c.CouponDescription, f => f.Lorem.Paragraph())
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
        }

        private void SeedRandomBookingData(ModelBuilder modelBuilder)
        {
            var bookingFaker = new Faker<Booking>();
            var serviceBookingFaker = new Faker<ServiceBooking>();

            for (int i = 1; i <= 15; i++)
            {
                bookingFaker.RuleFor(b => b.BookingId, i)
                    .RuleFor(b => b.BookingTime, f => f.Date.Soon())
                    .RuleFor(b => b.PaymentMethod, f => "Tra sau")
                    .RuleFor(b => b.PaymentStatus, f => f.PickRandom<PaymentStatus>())
                    .RuleFor(b => b.BookingStatus, f => f.PickRandom<BookingStatus>())
                    .RuleFor(b => b.CreatedAt, now)
                    .RuleFor(b => b.CarId, f => f.Random.Int(1, 20))
                    .RuleFor(b => b.GarageId, f => f.Random.Int(1, 25));

                modelBuilder.Entity<Booking>().HasData(bookingFaker.Generate());
            }

            for (int i = 1; i <= 50; i++)
            {
                serviceBookingFaker.RuleFor(s => s.ServiceBookingId, i)
                    .RuleFor(s => s.ProductCost, f => f.Random.Float(50, 200))
                    .RuleFor(s => s.ServiceCost, f => f.Random.Float(50, 200))
                    .RuleFor(s => s.BookingId, f => f.Random.Int(1, 15))
                    .RuleFor(s => s.ServiceId, f => f.Random.Int(1, 10))
                    .RuleFor(s => s.ProductId, f => f.Random.Int(1, 30))
                    .RuleFor(s => s.MechanicId, f => f.Random.Int(1, 19));

                modelBuilder.Entity<ServiceBooking>().HasData(serviceBookingFaker.Generate());
            }
        }

        private void SeedRandomReportData(ModelBuilder modelBuilder)
        {
            var reportFaker = new Faker<Report>();

            for (int i = 1; i <= 15; i++)
            {
                reportFaker.RuleFor(r => r.ReportId, i)
                    .RuleFor(r => r.Date, f => f.Date.Past())
                    .RuleFor(r => r.Notes, f => f.Lorem.Text())
                    .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
                    .RuleFor(r => r.ReportStatus, f => Status.Activate)
                    .RuleFor(r => r.CreatedAt, now);

                modelBuilder.Entity<Report>().HasData(reportFaker.Generate());
            }
        }
    }
}