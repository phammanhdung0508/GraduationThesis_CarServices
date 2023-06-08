using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Role;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Subcategory;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.WorkingSchedule;
using GraduationThesis_CarServices.Models.DTO.ServiceGarage;

namespace GraduationThesis_CarServices.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            //Authen
            CreateMap<User, LoginDto>();
            CreateMap<User, UserLoginDto>().ForMember(des => des.UserFullName,
                obj => obj.MapFrom(src => src.UserFirstName + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));


            //Role
            CreateMap<Role, RoleDto>();


            //Coupon
            CreateMap<Coupon, CouponGarageDto>()
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")))
                .ReverseMap()
                .ForMember(des => des.Garage, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Coupon, CouponListResponseDto>()
                .ForMember(des => des.CouponStatus, obj => obj.MapFrom(src => src.CouponStatus.ToString()))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")));
            CreateMap<Coupon, CouponDetailResponseDto>()
                .ForMember(des => des.CouponType, obj => obj.MapFrom(src => src.CouponType.ToString()))
                .ForMember(des => des.CouponStatus, obj => obj.MapFrom(src => src.CouponStatus.ToString()))
                .ForMember(des => des.CouponStartDate, obj => obj.MapFrom(src => src.CouponStartDate.ToString("MM/dd/yyyy")))
                .ForMember(des => des.CouponEndDate, obj => obj.MapFrom(src => src.CouponEndDate.ToString("MM/dd/yyyy")));
            CreateMap<CouponCreateRequestDto, Coupon>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequestDto>()
                .ForMember(des => des.CouponId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, CouponStatusRequestDto>().ReverseMap();


            //Garage
            CreateMap<Garage, GarageBookingDto>()
                .ReverseMap().ForMember(des => des.Bookings, obj => obj.Ignore());
            CreateMap<Garage, GarageReviewDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Garage, GarageListResponseDto>();
            CreateMap<Garage, GarageDetailResponseDto>()
                .ForMember(des => des.UserGarageDto, obj => obj.MapFrom(src => src.User))
                .ForMember(des => des.ReviewGarageDto, obj => obj.MapFrom(src => src.Reviews))
                .ForMember(des => des.CouponGarageDto, obj => obj.MapFrom(src => src.Coupons))
                .ForMember(des => des.GarageDetailGarageDto, obj => obj.MapFrom(src => src.GarageDetails));
            CreateMap<Garage, GarageCreateRequestDto>().ReverseMap();
            CreateMap<Garage, GarageUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, GarageStatusRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, LocationUpdateRequestDto>()
                .ForMember(des => des.GarageId, obj => obj.Ignore()).ReverseMap();


            //User
            CreateMap<User, UserReviewDto>();
            CreateMap<User, UserGarageDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
            CreateMap<User, UserMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName));
            CreateMap<User, UserDetailMechanicDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role))
                .ForMember(des => des.UserGender, obj => obj.MapFrom(src => src.UserGender.ToString()));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<User, UserListResponseDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role)).ReverseMap();
            CreateMap<User, UserDetailResponseDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role)).ReverseMap();
            CreateMap<User, UserCreateRequestDto>()
                .ForMember(des => des.UserPassword, obj => obj.Ignore())
                .ForMember(des => des.PasswordConfirm, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, UserUpdateRequestDto>()
                .ForMember(des => des.UserId, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, UserStatusRequestDto>()
                .ForMember(des => des.UserId, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, UserRoleRequestDto>()
                .ForMember(des => des.UserId, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, UserLocationRequestDto>()
                .ForMember(des => des.UserId, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, UserWorkingScheduleDto>()
                .ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ReverseMap();    

            //Mechanic
            CreateMap<Mechanic, MechanicWorkingScheduleDto>()
                .ForMember(des => des.UserWorkingScheduleDto, obj => obj.MapFrom(src => src.User));

            //Working Schedule
            CreateMap<WorkingSchedule, WorkingScheduleListResponseDto>();
            CreateMap<WorkingSchedule, WorkingScheduleByGarageDto>()
                .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic))
                .ReverseMap();
            CreateMap<WorkingSchedule, WorkingScheduleByMechanicDto>()
                .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage))
                .ReverseMap();
            CreateMap<WorkingSchedule, WorkingScheduleDetailResponseDto>()
                .ForMember(des => des.GarageWorkingScheduleDto, obj => obj.MapFrom(src => src.Garage))
                .ForMember(des => des.MechanicWorkingScheduleDto, obj => obj.MapFrom(src => src.Mechanic))
                .ReverseMap();
            CreateMap<WorkingSchedule, WorkingScheduleCreateRequestDto>().ReverseMap();
            CreateMap<WorkingSchedule, WorkingScheduleUpdateStatusDto>().ForMember(des => des.WorkingScheduleId, obj => obj.Ignore()).ReverseMap();


            //Customer
            CreateMap<Customer, CustomerReviewDto>()
                .ForMember(des => des.UserReviewDto, obj => obj.MapFrom(src => src.User));


            //Mechanic
            CreateMap<Mechanic, MechanicListResponseDto>()
                .ForMember(des => des.UserMechanicDto, obj => obj.MapFrom(src => src.User));
            CreateMap<Mechanic, MechanicDetailResponseDto>()
                .ForMember(des => des.UserDetailMechanicDto, obj => obj.MapFrom(src => src.User));


            //Working Schedule
            CreateMap<WorkingSchedule, WorkingScheduleListResponseDto>();

            //Review
            CreateMap<Review, ReviewGarageDto>().ReverseMap()
                .ForMember(des => des.Garage, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Review, ReviewListResponseDto>();
            CreateMap<Review, ReviewDetailResponseDto>()
                .ForMember(des => des.CustomerReviewDto, obj => obj.MapFrom(src => src.Customer))
                .ForMember(des => des.GarageReviewDto, obj => obj.MapFrom(src => src.Garage)).ReverseMap();;
            CreateMap<Review, ReviewCreateRequestDto>().ReverseMap();
            CreateMap<Review, ReviewUpdateRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, ReviewStatusRequestDto>()
                .ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();


            //ServiceGarage
            CreateMap<GarageDetail, GarageDetailGarageDto>()
                .ForMember(des => des.ServiceGarageDto, obj => obj.MapFrom(src => src.Service));
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<GarageDetail, ServiceGarageServiceDto>()
                .ForMember(des => des.GarageServiceDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<GarageDetail, ServiceGarageListResponseDto>()
                .ForMember(des => des.ServiceOfServiceGarageDto, obj => obj.MapFrom(src => src.Service));

            //ServiceBooking
            CreateMap<BookingDetail, ServiceListDto>()
                .ReverseMap()
                .ForMember(des => des.MechanicId, obj => obj.Ignore());

            //Service
            CreateMap<Service, ServiceGarageDto>().ReverseMap();
            CreateMap<Service, ServiceProductDto>().ReverseMap().ForMember(des => des.Products, obj => obj.Ignore());
            CreateMap<Service, ServiceOfServiceGarageDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ReverseMap();
            CreateMap<Service, ServiceListResponseDto>().ReverseMap();
            CreateMap<Service, ServiceDetailResponseDto>()
                .ForMember(des => des.ProductServiceDtos, obj => obj.MapFrom(src => src.Products))
                .ForMember(des => des.ServiceGarageServiceDtos, obj => obj.MapFrom(src => src.GarageDetails))
                .ReverseMap();
            CreateMap<Service, ServiceCreateRequestDto>().ReverseMap();
            CreateMap<Service, ServiceUpdateRequestDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Service, ServiceStatusRequestDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            //CreateMap<Service, DeleteServiceDto>().ReverseMap();

            //Report
            CreateMap<Report, ReportBookingDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, CreateReportDto>().ReverseMap();
            CreateMap<Report, UpdateReportDto>().ForMember(des => des.ReportId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Report, DeleteReportDto>().ReverseMap();


            //Car
            CreateMap<Car, CarBookingDto>()
                .ReverseMap().ForMember(des => des.Bookings, obj => obj.Ignore());
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Car, CarListResponseDto>().ReverseMap();
            CreateMap<Car, CarDetailResponseDto>().ReverseMap();
            CreateMap<Car, CarCreateRequestDto>().ReverseMap();
            CreateMap<Car, CarUpdateRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Car, CarStatusRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();

            // Subcategory
            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, SubcategoryProductDto>().ForMember(des => des.CategoryProductDto, obj => obj.MapFrom(src => src.Category)).ReverseMap();
            CreateMap<Subcategory, CreateSubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, UpdateSubcategoryDto>().ForMember(des => des.SubcategoryId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Subcategory, DeleteSubcategoryDto>().ReverseMap();


            // Category
            CreateMap<Category, CategoryListResponseDto>();
            CreateMap<Category, CategoryDetailResponseDto>();
            CreateMap<CategoryCreateRequestDto, Category>();
            CreateMap<CategoryUpdateRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());
            CreateMap<CategoryStatusRequestDto, Category>()
                .ForMember(des => des.CategoryId, obj => obj.Ignore());


            // Product
            CreateMap<Product, ProductServiceDto>().ReverseMap();
            CreateMap<Product, ProductListResponseDto>()
                .ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service))
                .ReverseMap();
            CreateMap<Product, ProductDetailResponseDto>()
                .ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service))
                .ReverseMap();
            CreateMap<Product, ProductCreateRequestDto>().ReverseMap();
            CreateMap<Product, ProductPriceRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductStatusRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductQuantityRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();

            //Booking
            CreateMap<Booking, BookingListResponseDto>()
                .ForMember(des => des.CarBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Booking, BookingDetailResponseDto>()
                .ForMember(des => des.CarBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<Booking, BookingCreateRequestDto>()
                .ReverseMap();
            CreateMap<Booking, BookingStatusRequestDto>()
                .ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();
        }
    }
}