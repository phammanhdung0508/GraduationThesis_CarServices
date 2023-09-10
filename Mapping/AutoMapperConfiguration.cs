using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Role;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
// using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using System.Text;
using System.Globalization;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.PaymentGateway;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //Paymnet
            CreateMap<PaymentRequest, Payment>();


            //Authen
            CreateMap<RefreshTokenDto, User>()
                .ForMember(des => des.RefreshToken, obj => obj.MapFrom(src => src.Token))
                .ForMember(des => des.RefreshTokenCreated, obj => obj.MapFrom(src => src.Created))
                .ForMember(des => des.RefreshTokenExpires, obj => obj.MapFrom(src => src.Expires));
            CreateMap<User, LoginDto>();
            CreateMap<User, UserLoginDto>().ForMember(des => des.UserFullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
            CreateMap<UserRegisterRequestDto, User>();

            //Role
            CreateMap<Role, RoleDto>();

            //Coupon
            CreateMap<Coupon, CouponGarageDto>()
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("dd/MM/yyyy")))
                .ReverseMap()
                .ForMember(des => des.Garage, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Coupon, CouponListResponseDto>()
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.GarageName, obj => obj.MapFrom(src => src.Garage.GarageName))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("dd/MM/yyyy")));
            CreateMap<Coupon, FilterCouponByGarageResponseDto>()
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("dd/MM/yyyy")))
                //.ForMember(des => des.CouponMinSpend, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.CouponValue) + " VND"))
                .ForMember(des => des.CouponType, obj => obj.MapFrom((src, des) =>
                {
                    switch (src.CouponType)
                    {
                        case CouponType.Percent:
                            return "Phần trăm.";
                        case CouponType.FixedAmount:
                            return "Giá cố định";
                    }
                    return "N/A";
                }))
                .ForMember(des => des.CouponValue, obj => obj.MapFrom((src, des) =>
                {
                    switch (src.CouponType)
                    {
                        case CouponType.Percent:
                            return "Giảm " + String.Format(CultureInfo.InvariantCulture, "{0:0}%", src.CouponValue);
                        case CouponType.FixedAmount:
                            return "Giảm " + FormatCurrency.FormatNumber(src.CouponValue) + " VND";
                    }
                    return "N/A";
                }));
            CreateMap<Coupon, CouponDetailResponseDto>()
                .ForMember(des => des.CouponType, obj => obj.MapFrom(src => src.CouponType.ToString()))
                .ForMember(des => des.CouponStatus, obj => obj.MapFrom(src => src.CouponStatus.ToString()))
                .ForMember(des => des.CouponValue, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.CouponValue) + " VND"))
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("dd/MM/yyyy")))
                .ForMember(des => des.CouponGarageInfoDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<CouponCreateRequestDto, Coupon>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequestDto>()
                .ForMember(des => des.CouponId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, CouponStatusRequestDto>().ReverseMap();


            //Garage
            CreateMap<Garage, GetIdAndNameDto>()
                .ForMember(des => des.Id, obj => obj.MapFrom(src => src.GarageId))
                .ForMember(des => des.Name, obj => obj.MapFrom(src => src.GarageName + ". " + src.GarageAddress));
            CreateMap<Lot, LotList>()
                .ForMember(des => des.IsAssignedFor, obj => obj.MapFrom(src => string.IsNullOrEmpty(src.IsAssignedFor) ? "Không có" : src.IsAssignedFor))
                .ForMember(des => des.LotStatus, obj => obj.MapFrom((src, des) => {
                    switch (src.LotStatus)
                    {
                        case LotStatus.Free:
                            return "Trống";
                        case LotStatus.Assigned:
                            return "Được sử dụng";
                        case LotStatus.BeingUsed:
                            return "Được sử dụng";
                    }
                    return "N/A";
                }));
            CreateMap<Garage, CouponGarageInfoDto>()
                .ForMember(des => des.Manager, obj => obj.MapFrom(src => src.User.UserFirstName + " " + src.User.UserLastName))
                .ForMember(des => des.GaragePhoneNumber, obj => obj.MapFrom(src => src.GarageContactInformation))
                .ForMember(des => des.GarageAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity));
            CreateMap<Garage, GarageBookingDto>()
                .ForMember(des => des.FullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity))
                .ReverseMap().ForMember(des => des.Bookings, obj => obj.Ignore());
            CreateMap<Garage, GarageAdminListResponseDto>()
                .ForMember(des => des.UserGarageDto, obj => obj.MapFrom(src => src.User));
            CreateMap<Garage, GarageReviewDto>();
            CreateMap<Garage, GarageWorkingScheduleDto>();
            CreateMap<Garage, GarageServiceDto>();
            CreateMap<Garage, GarageOfGarageDetailDto>();
            CreateMap<Garage, GarageListMobileMapResponseDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Garage, GarageListResponseDto>()
                .ForMember(des => des.GarageFullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity));
            CreateMap<Garage, GarageDetailResponseDto>()
                .ForMember(des => des.ManagerGarageDto, obj => obj.MapFrom(src => src.User))
                // .ForMember(des => des.CouponGarageDto, obj => obj.MapFrom(src => src.Coupons))
                // .ForMember(des => des.GarageDetailGarageDto, obj => obj.MapFrom(src => src.GarageDetails))
                .ForMember(des => des.GarageFullAddress, obj => obj.MapFrom(src => src.GarageAddress + ", " + src.GarageWard + ", " + src.GarageDistrict + ", " + src.GarageCity))
                .ForMember(des => des.HoursOfOperation, obj => obj.MapFrom(src => "Từ " + src!.OpenAt + " đến " + src.CloseAt))
                .ForMember(des => des.Rating, obj => obj.MapFrom((src, des) =>
                {
                    return src.Reviews.Count != 0 ? src.Reviews.Sum(r => r.Rating) / src.Reviews.Count : 0;
                }))
                .ForMember(des => des.IsOpen, obj => obj.MapFrom((src, des) =>
                {

                    var presentTime = DateTime.Now.TimeOfDay;
                    var openAt = DateTime.ParseExact(src.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var closeAt = DateTime.ParseExact(src.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var midnight = TimeSpan.Zero;

                    return presentTime switch
                    {
                        var time when TimeSpan.Compare(presentTime, openAt).Equals(1) && TimeSpan.Compare(presentTime, closeAt).Equals(-1) => "Open",
                        var time when TimeSpan.Compare(presentTime, closeAt).Equals(1) || TimeSpan.Compare(presentTime, openAt).Equals(-1) => "Open",
                        _ => "N/A",
                    };
                }));
            CreateMap<Garage, GarageCreateRequestDto>().ReverseMap();
            CreateMap<Garage, GarageUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, GarageStatusRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, LocationUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();


            //User
            CreateMap<User, GetIdAndNameDto>()
                .ForMember(des => des.Id, obj => obj.MapFrom(src => src.UserId))
                .ForMember(des => des.Name, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName));
            CreateMap<User, ManagerGarageDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)))
                .ForMember(des => des.UserStatus, obj => obj.MapFrom(src => src.UserStatus));
            CreateMap<Customer, UserCustomerDto>();
            CreateMap<User, UserBookingDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.Customer.User.UserFirstName + " " + src.Customer.User.UserLastName));
            CreateMap<User, MechanicBookingDetailDto>()
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
            CreateMap<User, CustomerBookingDto>()
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail))); ;
            CreateMap<User, UserReviewDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.Customer.User.UserFirstName + " " + src.Customer.User.UserLastName));
            CreateMap<User, UserGarageDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
            CreateMap<User, UserMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
            CreateMap<User, UserDetailMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserGender, obj => obj.MapFrom(src => src.UserGender.ToString()));
            CreateMap<User, CustomerListResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.Customer.User.UserFirstName + " " + src.Customer.User.UserLastName))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<User, UserListResponseDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserStatus, obj => obj.MapFrom(src => src.UserStatus.ToString()))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
            CreateMap<User, UserDetailResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserCustomerDto, obj => obj.MapFrom(src => src.Customer))
                .ForMember(des => des.UserGender, obj => obj.MapFrom(src => src.UserGender.ToString()))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)));
                //.ForMember(des => des.UserDateOfBirth, obj => obj.MapFrom(src => src.UserDateOfBirth!.Value.ToString("dd/MM/yyyy")));
            CreateMap<User, CustomerDetailResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.UserEmail, obj => obj.MapFrom(src => Base64Decode(src.UserEmail)))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("dd/MM/yyyy hh:mm tt")))
                .ForMember(des => des.UserCustomerDto, obj => obj.MapFrom(src => src.Customer));
            CreateMap<UserCreateRequestDto, User>()
                .ForMember(des => des.UserStatus, obj => obj.MapFrom(src => Status.Activate))
                .ForMember(des => des.EmailConfirmed, obj => obj.MapFrom(src => 1))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => DateTime.Now));
            CreateMap<UserUpdateRequestDto, User>();
            CreateMap<UserRoleRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<UserStatusRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<UserLocationRequestDto, User>()
                .ForMember(des => des.UserId, obj => obj.Ignore());
            CreateMap<User, UserWorkingScheduleDto>()
                .ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName));

            //Mechanic
            CreateMap<MechanicCreateRequestDto, User>();
            CreateMap<Mechanic, MechanicWorkForBookingResponseDto>()
                .ForMember(des => des.IsMainMechanic, obj => obj.MapFrom((src, des) =>
                {
                    return src.Level.Equals(MechanicLevel.Level3.ToString()) ? des.IsMainMechanic = true : des.IsMainMechanic = false;
                }))
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.User.UserFirstName + " " + src.User.UserLastName))
                .ForMember(des => des.Image, obj => obj.MapFrom(src => src.User.UserImage));
            CreateMap<Mechanic, MechanicWorkForGarageResponseDto>()
                .ForMember(des => des.FullName, obj => obj.MapFrom(src => src.User.UserFirstName + " " + src.User.UserLastName))
                .ForMember(des => des.Image, obj => obj.MapFrom(src => src.User.UserImage));

            // CreateMap<Mechanic, MechanicWorkingScheduleDto>()
            //     .ForMember(des => des.UserWorkingScheduleDto, obj => obj.MapFrom(src => src.User));
            //Working Schedule
            // CreateMap<GarageMechanic, WorkingScheduleListResponseDto>();
            // CreateMap<GarageMechanic, WorkingScheduleByGarageDto>()
            //     .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic))
            //     .ForMember(des => des.WorkingScheduleStatus, obj => obj.MapFrom(src => src.WorkingScheduleStatus.ToString()));
            // CreateMap<GarageMechanic, WorkingScheduleByMechanicDto>()
            //     .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage));
            // CreateMap<GarageMechanic, WorkingScheduleDetailResponseDto>()
            //     .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage))
            //     .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic));
            // CreateMap<GarageMechanic, WorkingScheduleCreateRequestDto>().ReverseMap();
            // CreateMap<GarageMechanic, WorkingScheduleUpdateStatusDto>().ForMember(des => des.WorkingScheduleId, obj => obj.Ignore()).ReverseMap();

            //Customer
            CreateMap<Customer, CustomerReviewDto>()
                .ForMember(des => des.UserReviewDto, obj => obj.MapFrom(src => src.User));
            CreateMap<Customer, UserCustomerDto>()
                .ForMember(des => des.CustomerCarDtos, obj => obj.MapFrom(src => src.Cars));


            //Mechanic
            CreateMap<Mechanic, MechanicListResponseDto>()
                .ForMember(des => des.UserMechanicDto, obj => obj.MapFrom(src => src.User))
                .ForMember(des => des.UserId, obj => obj.MapFrom(src => src.User.UserId));
            CreateMap<Mechanic, MechanicDetailResponseDto>()
                .ForMember(des => des.IsAvaliable, obj => obj.MapFrom(src => src.MechanicStatus.ToString()))
                .ForMember(des => des.UserDetailMechanicDto, obj => obj.MapFrom(src => src.User));

            //Review
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Review, ReviewListResponseDto>()
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => src.CreatedAt!.Value.ToString("dd/MM/yyyy")))
                .ForMember(des => des.UserReviewDto, obj => obj.MapFrom(src => src.Customer.User))
                .ForMember(des => des.GarageReviewDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Review, ReviewDetailResponseDto>()
                .ForMember(des => des.CustomerReviewDto, obj => obj.MapFrom(src => src.Customer))
                .ForMember(des => des.GarageReviewDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<ReviewCreateRequestDto, Review>();
            CreateMap<Review, ReviewUpdateRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, ReviewStatusRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();


            //GarageDetail
            CreateMap<GarageDetail, GarageDetailGarageDto>()
                .ForMember(des => des.ServiceGarageDto, obj => obj.MapFrom(src => src.Service));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<GarageDetail, GarageDetailServiceDto>()
                .ForMember(des => des.GarageServiceDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<GarageDetail, GarageDetailListResponseDto>()
                .ForMember(des => des.ServiceOfGarageDetailDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<GarageDetail, GarageDetailDetailResponseDto>()
                .ForMember(des => des.ServiceOfGarageDetailDto, obj => obj.MapFrom(src => src.Service))
                .ForMember(des => des.GarageOfGarageDetailDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<GarageDetail, GarageDetailCreateRequestDto>().ReverseMap();
            CreateMap<GarageDetail, GarageDetailUpdateRequestDto>().ForMember(des => des.GarageDetailId, obj => obj.Ignore()).ReverseMap();

            //ServiceDetail
            CreateMap<ServiceDetail, ServiceDetailServiceDto>()
                .ForMember(des => des.MinNumberOfCarLot, obj => obj.MapFrom(src => src.MinNumberOfCarLot + " chỗ."))
                .ForMember(des => des.MaxNumberOfCarLot, obj => obj.MapFrom(src => src.MaxNumberOfCarLot + " chỗ."))
                .ForMember(des => des.ServicePrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ServicePrice) + " VND"));
            CreateMap<ServiceDetail, ServiceDetailListResponseDto>()
                .ForMember(des => des.ServiceDetailDesc, obj => obj.MapFrom(src => "Xe từ " + src.MinNumberOfCarLot + " đến " + src.MaxNumberOfCarLot + " chỗ."))
                .ForMember(des => des.ServiceDetailPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ServicePrice) + " VND"));
            CreateMap<ServiceDetail, ServiceDetailCreateRequestDto>().ReverseMap();
            CreateMap<ServiceDetailUpdateRequestDto, ServiceDetail>()
                .ForMember(des => des.ServicePrice, obj => obj.MapFrom(src => FormatCurrency.ConvertCurrencyStringToDecimal(src.ServicePrice!)));

            //Service
            CreateMap<Service, GetIdAndNameDto>()
                .ForMember(des => des.Id, obj => obj.MapFrom(src => src.ServiceId))
                .ForMember(des => des.Name, obj => obj.MapFrom(src => src.ServiceName));
            CreateMap<Service, ServiceOfServiceDetailDto>()
                .ForMember(des => des.ServiceStatus, obj => obj.MapFrom(src => src.ServiceStatus.ToString()))
                .ForMember(des => des.ServiceDetailListResponseDtos, obj => obj.MapFrom(src => src.ServiceDetails));
            CreateMap<Service, ServicListDto>();
            CreateMap<Service, ServiceGarageDto>();
            CreateMap<Service, ServiceProductDto>();
            CreateMap<Service, ServiceListMobileResponseDto>();
            CreateMap<Service, ServiceOfGarageDetailDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ForMember(des => des.ServiceDetailServiceDtos, obj => obj.MapFrom(src => src.ServiceDetails));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Service, ServiceListResponseDto>()
                .ForMember(des => des.ServiceUnit, obj => obj.MapFrom((src, des) => {
                    switch (src.ServiceUnit)
                    {
                        case ServiceUnit.Time:
                            return des.ServiceUnit = "LẦN";
                        case ServiceUnit.Pack:
                            return des.ServiceUnit = "GÓI";
                        default: return "N/A";
                    }
                }));
            CreateMap<Service, ServiceDetailResponseDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ForMember(des => des.ServiceDetailServiceDtos, obj => obj.MapFrom(src => src.ServiceDetails))
                .ForMember(des => des.GarageDetailServiceDtos, obj => obj.MapFrom(src => src.GarageDetails))
                .ForMember(des => des.ServiceStatus, obj => obj.MapFrom(src => src.ServiceStatus.ToString()));
            CreateMap<Service, ServiceCreateRequestDto>().ReverseMap();
            CreateMap<ServiceUpdateRequestDto, Service>()
                .ForMember(des => des.ServiceUnit, obj => obj.MapFrom(src => src.ServiceUnit))
                .ForMember(des => des.UpdatedAt, des => des.MapFrom(src => DateTime.Now))
                .ForMember(des => des.ServiceId, obj => obj.Ignore());
            CreateMap<Service, ServiceStatusRequestDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            //CreateMap<Service, DeleteServiceDto>().ReverseMap();

            //Report
            // CreateMap<Report, ReportBookingDto>();
            //----------------------------------------------------------------------------------------------------------------------
            // CreateMap<Report, ReportDto>().ReverseMap();
            // CreateMap<Report, CreateReportDto>().ReverseMap();
            // CreateMap<Report, UpdateReportDto>().ForMember(des => des.ReportId, obj => obj.Ignore()).ReverseMap();
            // CreateMap<Report, DeleteReportDto>().ReverseMap();


            //Car
            CreateMap<UserCreateRequestDto, Car>()
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => DateTime.Now))
                .ForMember(des => des.CarStatus, obj => obj.MapFrom(src => Status.Activate));
            CreateMap<Car, CustomerCarDto>();
            CreateMap<Car, CarBookingDetailForStaffDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Car, CarListResponseDto>()
                .ForMember(des => des.CarStatus, obj => obj.MapFrom(src => src.CarBookingStatus.ToString()));
            CreateMap<Car, CarDetailResponseDto>().ReverseMap();
            CreateMap<Car, CarCreateRequestDto>().ReverseMap();
            CreateMap<Car, CarUpdateRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Car, CarStatusRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();

            // // Subcategory
            // CreateMap<Subcategory, SubcategoryDto>()
            //     .ForMember(des => des.SubcategoryStatus, obj => obj.MapFrom(src => src.SubcategoryStatus.ToString()));
            // CreateMap<Subcategory, SubcategoryProductDto>()
            //     .ForMember(des => des.CategoryProductDto, obj => obj.MapFrom(src => src.Category));
            // CreateMap<Subcategory, CreateSubcategoryDto>().ReverseMap();
            // CreateMap<Subcategory, UpdateSubcategoryDto>().ForMember(des => des.SubcategoryId, obj => obj.Ignore()).ReverseMap();
            // CreateMap<Subcategory, DeleteSubcategoryDto>().ReverseMap();


            // Category
            CreateMap<Category, CategoryListResponseDto>()
                .ForMember(des => des.CategoryStatus, obj => obj.MapFrom(src => src.CategoryStatus.ToString()));
            CreateMap<Category, CategoryDetailResponseDto>()
                .ForMember(des => des.CategoryStatus, obj => obj.MapFrom(src => src.CategoryStatus.ToString()));
            CreateMap<CategoryCreateRequestDto, Category>();
            CreateMap<CategoryUpdateRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());
            CreateMap<CategoryStatusRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());
            CreateMap<Category, CategoryProductDto>();


            // Product
            CreateMap<Product, ProductBookingDetailDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Product, ProductServiceDto>()
                .ForMember(des => des.ProductPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ProductPrice) + " VND"));
            CreateMap<Product, ProductListResponseDto>()
                //.ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ProductUnit, obj => obj.MapFrom(src => src.ProductUnit.ToString()))
                .ForMember(des => des.ProductPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ProductPrice) + " VND"))
                .ForMember(des => des.CategoryProductDto, obj => obj.MapFrom(src => src.Category));
            CreateMap<Product, ProductDetailResponseDto>()
                //.ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ProductUnit, obj => obj.MapFrom(src => src.ProductUnit.ToString()))
                .ForMember(des => des.ProductPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ProductPrice) + " VND"))
                .ForMember(des => des.ProductStatus, obj => obj.MapFrom(src => src.ProductStatus.ToString()))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<ProductCreateRequestDto, Product>();
            CreateMap<ProductUpdateRequestDto, Product>()
                .ForMember(des => des.ProductPrice, obj => obj.MapFrom(src => FormatCurrency.ConvertCurrencyStringToDecimal(src.ProductPrice!)))
                .ForMember(des => des.UpdatedAt, des => des.MapFrom(src => DateTime.Now))
                .ForMember(des => des.ProductId, obj => obj.Ignore());
            CreateMap<Product, ProductStatusRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();

            //ServiceBooking
            CreateMap<BookingDetail, ServiceStatusForStaffDto>()
                .ForMember(des => des.ServiceName, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceName));
            CreateMap<BookingDetail, BookingDetailStatusForBookingResponseDto>()
                .ForMember(des => des.ServiceName, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceName))
                .ForMember(des => des.UpdatedAt, obj => obj.MapFrom(src => src.UpdatedAt));
            CreateMap<BookingDetail, ServiceListBookingDetailDto>()
                .ForMember(des => des.ServicePrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ServicePrice) + " VND"))
                .ForMember(des => des.ServiceName, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceName));
            CreateMap<BookingDetail, BookingDetailDto>()
                // .ForMember(des => des.MechanicBookingDetailDto, obj => obj.MapFrom(src => src.Booking.BookingMechanics))
                // .ForPath(des => des.MechanicBookingDetailDto.FullName, obj => obj.MapFrom(src => src.Mechanic.User.UserFirstName + " " + src.Mechanic.User.UserLastName))
                .ForMember(des => des.ServiceCost, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ServicePrice) + " VND"))
                .ForMember(des => des.ProductCost, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.ProductPrice) + " VND"))
                .ForMember(des => des.ProductBookingDetailDto, obj => obj.MapFrom(src => src.Product))
                .ForMember(des => des.BookingDetailStatus, obj => obj.MapFrom(src => src.BookingServiceStatus))
                .ForMember(des => des.ServiceBookingDetailDto, obj => obj.MapFrom(src => src.ServiceDetail))
                .ForPath(des => des.ServiceBookingDetailDto!.ServiceName, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceName))
                .ForPath(des => des.ServiceBookingDetailDto!.ServiceImage, obj => obj.MapFrom(src => src.ServiceDetail.Service.ServiceImage));


            CreateMap<ServiceDetail, ServiceBookingDetailDto>();


            //BookingMechanic
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<BookingMechanic, MechanicServiceStatusForStaffDto>()
                .ForMember(des => des.IsMainMechanic, obj => obj.MapFrom((src, des) =>
                {
                    return src.Mechanic.Level.Equals(MechanicLevel.Level3.ToString()) ? des.IsMainMechanic = true : des.IsMainMechanic = false;
                }))
                .ForMember(des => des.MechanicName, obj => obj.MapFrom(src => src.Mechanic.User.UserFirstName))
                .ForMember(des => des.MechanicImage, obj => obj.MapFrom(src => src.Mechanic.User.UserImage));
            CreateMap<EditMechanicBookingRequestDto, BookingMechanic>()
                .ForMember(des => des.BookingMechanicStatus, obj => obj.MapFrom(src => Status.Activate))
                .ForMember(des => des.WorkingDate, obj => obj.MapFrom(src => DateTime.Now));

            //Booking
            CreateMap<Booking, BookingMechanicCurrentWorkingOn>()
                .ForMember(des => des.BookingTime, obj => obj.MapFrom(src => src.BookingTime.ToString("dd/MM/yyyy h:mm tt")))
                .ForMember(des => des.CarInfo, obj => obj.MapFrom(src => src.Car.CarBrand))
                .ForMember(des => des.GarageName, obj => obj.MapFrom(src => src.Garage.GarageName));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Booking, BookingServiceStatusForStaffResponseDto>()
                .ForMember(des => des.CarName, obj => obj.MapFrom(src => src.Car.CarBrand))
                .ForMember(des => des.BookingDay, obj => obj.MapFrom(src => src.BookingTime.ToString("dd-MM-yyyy")))
                .ForMember(des => des.Duration, obj => obj.MapFrom(src => "Từ " + src.BookingTime.ToString("h tt") +
                " Đến " + src.BookingTime.AddHours(src.CustomersCanReceiveTheCarTime).ToString("h tt")))
                .ForMember(des => des.CustomerName, obj => obj.MapFrom(src => src.Car.Customer.User.UserLastName + " "
                + src.Car.Customer.User.UserFirstName))
                .ForMember(des => des.CustomerImage, obj => obj.MapFrom(src => src.Car.Customer.User.UserImage))
                .ForMember(des => des.MechanicServiceStatusForStaffDtos, obj => obj.MapFrom(src => src.BookingMechanics))
                .ForMember(des => des.ServiceStatusForStaffDtos, obj => obj.MapFrom(src => src.BookingDetails));
            CreateMap<Booking, BookingListForStaffResponseDto>()
                .ForMember(des => des.CarLicensePlate, obj => obj.MapFrom(src => src.Car.CarLicensePlate))
                .ForMember(des => des.BookingStatus, obj => obj.MapFrom(src => src.BookingStatus.ToString()))
                .ForMember(des => des.BookingDuration, obj => obj.MapFrom(src => src.BookingTime.ToString("hh:tt") + " - " + src.BookingTime.AddHours(src.CustomersCanReceiveTheCarTime).ToString("hh:tt")));
            CreateMap<Booking, FilterByBookingStatusResponseDto>()
                .ForMember(des => des.BookingTime, obj => obj.MapFrom(src => src.BookingTime.ToString("dd/MM/yyyy")))
                .ForMember(des => des.CarName, obj => obj.MapFrom(src => src.Car.CarBrand))
                .ForMember(des => des.GarageAddress, obj => obj.MapFrom(src => src.Garage.GarageAddress + ", " + src.Garage.GarageWard + ", " + src.Garage.GarageDistrict + ", " + src.Garage.GarageCity))
                .ForMember(des => des.Price, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.FinalPrice) + " VND"));
            CreateMap<Booking, BookingDetailForCustomerResponseDto>()
                .ForMember(des => des.CarName, obj => obj.MapFrom(src => src.Car.CarBrand))
                .ForMember(des => des.DeviceToken, obj => obj.MapFrom(src => src.Car.Customer.User.DeviceToken))
                .ForMember(des => des.CustomerName, obj => obj.MapFrom(src => src.CustomerName))
                .ForMember(des => des.CarLicensePlate, obj => obj.MapFrom(src => src.Car.CarLicensePlate))
                .ForMember(des => des.Duration, obj => obj.MapFrom(src => "Từ " + src.BookingTime.ToString("h tt") + " đến " + src.BookingTime.AddHours(src.CustomersCanReceiveTheCarTime).ToString("h tt")))
                .ForMember(des => des.BookingDay, obj => obj.MapFrom(src => src.BookingTime.ToString("dd-MM-yyyy")))
                .ForMember(des => des.GaragePhone, obj => obj.MapFrom(src => src.Garage.GarageContactInformation))
                .ForMember(des => des.GarageAddress, obj => obj.MapFrom(src => src.Garage.GarageAddress + ", " + src.Garage.GarageWard + ", " + src.Garage.GarageDistrict + ", " + src.Garage.GarageCity))
                .ForMember(des => des.DiscountPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.DiscountPrice) + " VND"))
                .ForMember(des => des.TotalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.OriginalPrice) + " VND"))
                .ForMember(des => des.FinalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.FinalPrice) + " VND"));
            CreateMap<Booking, BookingDetailForStaffResponseDto>()
                .ForMember(des => des.DeviceToken, obj => obj.MapFrom(src => src.Car.Customer.User.DeviceToken))
                .ForMember(des => des.carBookingDetailForStaffDto, obj => obj.MapFrom(src => src.Car))
                .ForPath(des => des.carBookingDetailForStaffDto!.CarName, obj => obj.MapFrom(src => src.Car.CarBrand))
                .ForPath(des => des.carBookingDetailForStaffDto!.CarLicensePlate, obj => obj.MapFrom(src => src.Car.CarLicensePlate))
                .ForMember(des => des.CustomerName, obj => obj.MapFrom(src => src.CustomerName))
                .ForMember(des => des.CustomerPhone, obj => obj.MapFrom(src => src.CustomerPhone))
                .ForMember(des => des.CustomerAddress, obj => obj.MapFrom((src, des) =>
                {
                    if (src.Car.Customer.CustomerAddress is not null &&
                    src.Car.Customer.CustomerWard is not null &&
                    src.Car.Customer.CustomerDistrict is not null &&
                    src.Car.Customer.CustomerCity is not null)
                    {
                        return src.Car.Customer.CustomerAddress + ", " + src.Car.Customer.CustomerWard + ", " + src.Car.Customer.CustomerDistrict + ", " + src.Car.Customer.CustomerCity;
                    }
                    else
                    {
                        return "N/A";
                    }
                }))
                .ForMember(des => des.PickUpTime, obj => obj.MapFrom(src => src.BookingTime.ToString("hh:mm tt")))
                .ForMember(des => des.DeliveryTime, obj => obj.MapFrom(src => src.BookingTime.AddHours(src.CustomersCanReceiveTheCarTime).ToString("hh:mm tt")))
                .ForMember(des => des.DiscountPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.DiscountPrice) + " VND"))
                .ForMember(des => des.TotalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.OriginalPrice) + " VND"))
                .ForMember(des => des.FinalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.FinalPrice) + " VND"));
            CreateMap<Booking, FilterByCustomerResponseDto>()
                .ForMember(des => des.TotalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.TotalPrice) + " VND"))
                .ForMember(des => des.BookingTime, obj => obj.MapFrom(src => src.BookingTime.ToString("dd/MM/yyyy h:mm tt")));
            CreateMap<Booking, BookingListResponseDto>()
                .ForMember(des => des.TotalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.FinalPrice + 100) + " VND"))
                .ForMember(des => des.UpdatedAt, obj => obj.MapFrom(src => src.UpdatedAt!.Value.ToString("dd/MM/yyyy")))
                .ForMember(des => des.UserBookingDto, obj => obj.MapFrom(src => src.Car.Customer.User))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Booking, BookingDetailResponseDto>()
                // .ForMember(des => des.UserBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.BookingTime, obj => obj.MapFrom(src => src.BookingTime.ToString("dd/MM/yyyy h:mm tt")))
                .ForMember(des => des.OriginalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.OriginalPrice) + " VND"))
                .ForMember(des => des.DiscountPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.DiscountPrice) + " VND"))
                .ForMember(des => des.TotalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.TotalPrice) + " VND"))
                .ForMember(des => des.FinalPrice, obj => obj.MapFrom(src => FormatCurrency.FormatNumber(src.FinalPrice) + " VND"))
                .ForMember(des => des.CustomerBookingDto, obj => obj.MapFrom(src => src.Car.Customer.User))
                .ForPath(des => des.CustomerBookingDto!.FullName, obj => obj.MapFrom(src => src.Car.Customer.User.UserFirstName + " " + src.Car.Customer.User.UserLastName))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage))
                .ForMember(des => des.BookingDetailDtos, obj => obj.MapFrom(src => src.BookingDetails));
            CreateMap<BookingCreateRequestDto, Booking>()
                .ForMember(des => des.BookingCode, obj => obj.MapFrom(src => DateTime.Now.Ticks.ToString()))
                .ForMember(des => des.PaymentStatus, obj => obj.MapFrom(src => PaymentStatus.Unpaid))
                .ForMember(des => des.BookingStatus, obj => obj.MapFrom(src => BookingStatus.Canceled))
                .ForMember(des => des.IsAccepted, obj => obj.MapFrom(src => false))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => DateTime.Now));
            CreateMap<BookingCreateForManagerRequestDto, Booking>()
                .ForMember(des => des.BookingCode, obj => obj.MapFrom(src => DateTime.Now.Ticks.ToString()))
                .ForMember(des => des.PaymentStatus, obj => obj.MapFrom(src => PaymentStatus.Unpaid))
                .ForMember(des => des.BookingStatus, obj => obj.MapFrom(src => BookingStatus.Pending))
                .ForMember(des => des.IsAccepted, obj => obj.MapFrom(src => false))
                .ForMember(des => des.CreatedAt, obj => obj.MapFrom(src => DateTime.Now));
            CreateMap<Booking, BookingStatusRequestDto>().ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();
        }

        public string Base64Decode(string decodeStr)
        {
            var strBytes = Convert.FromBase64String(decodeStr);
            return Encoding.UTF8.GetString(strBytes);
        }
    }
}