#nullable disable
using System;
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
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<GarageDetail> GarageDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<GarageMechanic> GarageMechanics { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }

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
            this.SeedServiceData(modelBuilder);
            this.SeedServiceDetailData(modelBuilder);
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
            // modelBuilder.Entity<Booking>()
            // .HasOne(b => b.Report).WithOne(r => r.Booking)
            // .HasForeignKey<Report>(e => e.ReportId)
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

            // modelBuilder.Entity<Service>().OwnsOne(x => x.ServiceGroup);
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
                new Role{RoleId=5, RoleName="Staff"},
            };
            modelBuilder.Entity<Role>().HasData(list);
        }

        private void SeedCategoryData(ModelBuilder modelBuilder)
        {
            var list = new List<Category>{
                new Category{CategoryId=1, CategoryName="Sản phẩm vệ sinh", CreatedAt=now, CategoryStatus=Status.Activate},
                new Category{CategoryId=2, CategoryName="Sản phẩm nâng cấp", CreatedAt=now, CategoryStatus=Status.Activate}
            };
            modelBuilder.Entity<Category>().HasData(list);
        }

        private void SeedServiceData(ModelBuilder modelBuilder)
        {
            var list = new List<Service>{
                //GÓI DỊCH VỤ VỆ SINH + BẢO DƯỠNG
                new Service{ServiceId=1, ServiceName="Rửa xe + hút bụi + xịt gầm", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString().ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=2, ServiceName="Tẩy nhựa đường", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=3, ServiceName="Tẩy ố kính", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=4, ServiceName="Vệ Sinh + Bảo dưỡng khoang động cơ", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=5, ServiceName="Vệ Sinh + Bảo dưỡng nội thất", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=6, ServiceName="Vệ sinh nội soi hệ thống lạnh", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=7, ServiceName="Vệ sinh kim phun", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=8, ServiceName="Diệt khuẩn Demi", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Time,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=9, ServiceName="Diệt khuẩn khử mùi nội thất", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=10, ServiceName="Vệ sinh buồng đốt", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=11, ServiceName="Vệ sinh họng ga+ bướm ga+ van EGR", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=12, ServiceName="Vệ sinh họng ga+ bướm ga+ van EGR", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=13, ServiceName="Vệ sinh, bảo dưỡng thắng", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=14, ServiceName="Vệ sinh nội soi dàn lạnh", ServiceImage="https://www.shutterstock.com/image-vector/automotive-repair-icon-car-service-600w-431732104.jpg",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageCleaningMaintenance.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                //GÓI DỊCH VỤ NGOẠI THẤT
                new Service{ServiceId=15, ServiceName="Phủ Nano", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=4,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=16, ServiceName="Phủ Ceramic 9H", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=4,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=17, ServiceName="Phủ gầm gói tiêu chuẩn", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=18, ServiceName="Phủ gầm gói cao cấp", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=19, ServiceName="Dán phim Nano gói tiêu chuẩn", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=20, ServiceName="Dán phim Nano gói cao cấp", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=21, ServiceName="Phim 3M- Llumar gói tiêu chuẩn", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=22, ServiceName="Phim 3M- Llumar gói cao cấp", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                //Bảo dưỡng định kỳ
                new Service{ServiceId=23, ServiceName="Thay dầu, bộ lọc", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=1,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=24, ServiceName="Kiểm tra hệ thống điện, phanh, treo", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=25, ServiceName="Kiểm tra và thay bình ắc quy, bạc đạn, dây đai", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageExterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                //Sửa chữa khẩn cấp
                new Service{ServiceId=26, ServiceName="Áo ghế simili", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=27, ServiceName="Thảm lót sàn", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=3,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=28, ServiceName="Mặt cốp + lưng ghế", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=29, ServiceName="La phông trần - bọc ni long", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=30, ServiceName="Bọc da bò", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},

                new Service{ServiceId=31, ServiceName="Camera hành trình", ServiceImage="",
                    ServiceDetailDescription="Lorem Ipsum is simply dummy text.", ServiceDuration=2,
                    ServiceGroup = ServiceGroup.PackageInterior.ToString(), ServiceUnit = ServiceUnit.Pack,
                    ServiceStatus=Status.Activate, CreatedAt=now},
            };
            modelBuilder.Entity<Service>().HasData(list);
        }

        private void SeedServiceDetailData(ModelBuilder modelBuilder)
        {
            var list = new List<ServiceDetail>();
            Random random = new Random();
            int y = 1;
            for (int i = 1; i <= 62; i++)
            {
                int million = random.Next(1, 4);
                int hundred = random.Next(1, 9);
                var price = Double.Parse($"{million:N0}{hundred}00000");
                list.Add(new ServiceDetail { ServiceDetailId = i, MinNumberOfCarLot = 4, MaxNumberOfCarLot = 5, ServicePrice = price, ServiceId = y });
                price = price + 200000; 
                list.Add(new ServiceDetail { ServiceDetailId = i + 1, MinNumberOfCarLot = 6, MaxNumberOfCarLot = 7, ServicePrice = price, ServiceId = y });
                i = i + 1;
                y = y + 1;
            }
            modelBuilder.Entity<ServiceDetail>().HasData(list);
        }

        private void SeedRandomProductData(ModelBuilder modelBuilder)
        {
            var list = new List<Product>{
                new Product{ProductId = 1, ProductName="Oil System Cleaner (Vệ sinh động cơ) 250ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=280000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=4, CategoryId=1},
                new Product{ProductId = 2, ProductName="Fuel System Cleaner (Vệ sinh hệ thống xăng) 250ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=295000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=11, CategoryId=1},
                new Product{ProductId = 3, ProductName="Diesel System Cleaner (Vệ sinh hệ thống dầu) 350ml ", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=350000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=11, CategoryId=1},
                new Product{ProductId = 4, ProductName="Nano Engine Super Protection (Nano bảo vệ động cơ) 250ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=375000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=15, CategoryId=2},
                new Product{ProductId = 5, ProductName="Oxicat Oxygen Sensor & Catalytic (Vệ sinh cảm biến oxy và catalytic) 300ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=295000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=8, CategoryId=1},
                new Product{ProductId = 6, ProductName="Octane Booster (Cải thiện octane) 250ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=255000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=8, CategoryId=2},
                new Product{ProductId = 7, ProductName="Throttle Body Cleaner (Vệ sinh họng ga) 280ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=205000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=12, CategoryId=1},
                new Product{ProductId = 8, ProductName="Radiator Flush (Vệ sinh hệ thống làm mát) 300ml", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=155000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=7, CategoryId=1},
                new Product{ProductId = 9, ProductName="Radiator conditioner ( điều hòa tản nhiệt)", ProductImage="",
                    ProductUnit=ProductUnit.Bottle, ProductPrice=215000, ProductQuantity=100, ProductStatus=Status.Activate, ServiceId=14, CategoryId=1},
            };
            modelBuilder.Entity<Product>().HasData(list);
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
                            .RuleFor(m => m.TotalWorkingHours, f => f.Random.Int(0, 20));

                        modelBuilder.Entity<Mechanic>().HasData(mechanicFaker.Generate());
                        break;
                }

                userFaker.RuleFor(u => u.UserId, i)
                    .RuleFor(u => u.UserFirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.UserLastName, f => f.Name.LastName())
                    .RuleFor(u => u.UserEmail, (f, u) => encryptConfiguration.Base64Encode(f.Internet.Email(u.UserFirstName, u.UserLastName)))
                    .RuleFor(u => u.PasswordHash, password_hash)
                    .RuleFor(u => u.PasswordSalt, password_salt)
                    .RuleFor(u => u.UserImage, f => f.Internet.Avatar())
                    .RuleFor(u => u.UserPhone, f => f.Phone.PhoneNumberFormat())
                    .RuleFor(u => u.UserGender, f => f.PickRandom<Gender>())
                    .RuleFor(u => u.UserDateOfBirth, f => f.Person.DateOfBirth)
                    .RuleFor(u => u.UserBio, f => f.Lorem.Lines())
                    .RuleFor(u => u.UserStatus, Status.Activate)
                    .RuleFor(u => u.EmailConfirmed, 1)
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
            var garageMechanicFaker = new Faker<GarageMechanic>();

            for (int i = 1; i <= 60; i++)
            {
                garageMechanicFaker.RuleFor(w => w.GarageMechanicId, i)
                    .RuleFor(w => w.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(s => s.MechanicId, f => f.Random.Int(1, 18));

                modelBuilder.Entity<GarageMechanic>().HasData(garageMechanicFaker.Generate());
            }
        }

        private void SeedRandomCarData(ModelBuilder modelBuilder)
        {
            var carFaker = new Faker<Car>();

            for (int i = 1; i <= 20; i++)
            {
                carFaker.RuleFor(c => c.CarId, i)
                    .RuleFor(c => c.CarModel, f => f.Vehicle.Model())
                    .RuleFor(c => c.CarBrand, f => f.Vehicle.Manufacturer())
                    .RuleFor(c => c.CarLicensePlate, f => f.Random.Replace("##?-###.##"))
                    .RuleFor(c => c.CarFuelType, f => f.Vehicle.Fuel())
                    .RuleFor(c => c.CarDescription, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.NumberOfCarLot, f => f.Random.Int(2, 9))
                    .RuleFor(c => c.CarStatus, Status.Activate)
                    .RuleFor(c => c.CreatedAt, now)
                    .RuleFor(c => c.CustomerId, f => f.Random.Int(1, 20));

                modelBuilder.Entity<Car>().HasData(carFaker.Generate());
            }
        }

        private void SeedRandomGarageData(ModelBuilder modelBuilder)
        {
            var garageList = new List<Garage>();
            var garageFaker = new Faker<Garage>();
            var garageDetailFaker = new Faker<GarageDetail>();
            var lotFaker = new Faker<Lot>();

            for (int i = 1; i <= 14; i++)
            {
                garageFaker.RuleFor(g => g.GarageId, i)
                    .RuleFor(g => g.GarageName, "Me " + "Garage")
                    .RuleFor(g => g.GarageAbout, f => f.Lorem.Paragraph())
                    .RuleFor(g => g.GarageImage, f => f.Image.PicsumUrl())
                    .RuleFor(g => g.GarageContactInformation, f => f.Random.Replace("####.###.###"))
                    // .RuleFor(g => g.FromTo, "Monday to Saturday")
                    .RuleFor(g => g.OpenAt, "08:00 AM")
                    .RuleFor(g => g.CloseAt, "05:00 PM")
                    .RuleFor(g => g.GarageStatus, Status.Activate)
                    .RuleFor(g => g.CreatedAt, now)
                    .RuleFor(g => g.UserId, f => f.Random.Int(21, 30));

                var garage = garageFaker.Generate();

                var ran = RandomConfiguration.Location[i];

                garage.GarageAddress = ran.Address;
                garage.GarageWard = ran.Ward;
                garage.GarageDistrict = ran.District;
                garage.GarageCity = ran.City;
                garage.GarageLatitude = ran.Latitude;
                garage.GarageLongitude = ran.Longitude;

                modelBuilder.Entity<Garage>().HasData(garage);
            }

            for (int i = 1; i <= 70; i++)
            {
                garageDetailFaker.RuleFor(s => s.GarageDetailId, i)
                    .RuleFor(s => s.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(s => s.ServiceId, f => f.Random.Int(1, 10));

                modelBuilder.Entity<GarageDetail>().HasData(garageDetailFaker.Generate());
            }

            for (int i = 1; i <= 100; i++)
            {
                lotFaker.RuleFor(l => l.LotId, i)
                    .RuleFor(l => l.LotNumber, f => f.Random.Replace("#?"))
                    .RuleFor(l => l.LotStatus, f => f.PickRandom<LotStatus>())
                    .RuleFor(l => l.GarageId, f => f.Random.Int(1, 14));

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
                    .RuleFor(r => r.GarageId, f => f.Random.Int(1, 14))
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
                    .RuleFor(c => c.CouponValue, (f, g) =>
                    {
                        switch (g.CouponType)
                        {
                            case CouponType.Percent:
                                return f.Random.Int(1, 10);
                            case CouponType.FixedAmount:
                                return f.Random.Int(50, 100);
                        }
                        return 0;
                    })
                    .RuleFor(c => c.CouponStartDate, f => f.Date.Recent())
                    .RuleFor(c => c.CouponEndDate, f => f.Date.Soon())
                    .RuleFor(c => c.CouponMinSpend, f => f.Random.Float(1, 20))
                    .RuleFor(c => c.CouponMaxSpend, f => f.Random.Float(60, 100))
                    .RuleFor(c => c.NumberOfTimesToUse, f => f.Random.Int(1, 10))
                    .RuleFor(c => c.CouponStatus, f => f.PickRandom<CouponStatus>())
                    .RuleFor(c => c.GarageId, f => f.Random.Int(1, 14))
                    .RuleFor(c => c.CreatedAt, now);

                modelBuilder.Entity<Coupon>().HasData(couponFaker.Generate());
            }
        }

        private void SeedRandomBookingData(ModelBuilder modelBuilder)
        {
            var bookingFaker = new Faker<Booking>();
            var bookingDetailFaker = new Faker<BookingDetail>();

            for (int i = 1; i <= 15; i++)
            {
                bookingFaker.RuleFor(b => b.BookingId, i)
                    .RuleFor(b => b.BookingCode, f => f.Random.Replace("##?#???#?"))
                    .RuleFor(b => b.BookingTime, f => f.Date.Soon())
                    .RuleFor(b => b.PaymentMethod, f => "Tra sau")
                    .RuleFor(b => b.TotalPrice, f => f.Random.Double(1000000, 2000000))
                    .RuleFor(b => b.PaymentStatus, f => f.PickRandom<PaymentStatus>())
                    .RuleFor(b => b.BookingStatus, f => f.PickRandom<BookingStatus>())
                    .RuleFor(b => b.CreatedAt, now)
                    .RuleFor(b => b.CarId, f => f.Random.Int(1, 20))
                    .RuleFor(b => b.GarageId, f => f.Random.Int(1, 14));

                modelBuilder.Entity<Booking>().HasData(bookingFaker.Generate());
            }

            for (int i = 1; i <= 50; i++)
            {
                bookingDetailFaker.RuleFor(s => s.BookingDetailId, i)
                    .RuleFor(s => s.ProductCost, f => f.Random.Float(50, 200))
                    .RuleFor(s => s.ServiceCost, f => f.Random.Float(50, 200))
                    .RuleFor(s => s.BookingId, f => f.Random.Int(1, 15))
                    .RuleFor(s => s.ServiceDetailId, f => f.Random.Int(1, 3))
                    .RuleFor(s => s.ProductId, f => f.Random.Int(1, 9))
                    .RuleFor(s => s.MechanicId, f => f.Random.Int(1, 19));

                modelBuilder.Entity<BookingDetail>().HasData(bookingDetailFaker.Generate());
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