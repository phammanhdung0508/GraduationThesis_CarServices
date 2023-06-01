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
            CreateMap<CouponGarageDto, Coupon>()
                .ForMember(des => des.Garage, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, CouponListResponseDto>().ReverseMap();
            CreateMap<Coupon, CouponDetailResponseDto>().ReverseMap();
            CreateMap<Coupon, CouponCreateRequestDto>().ReverseMap();
            CreateMap<Coupon, CouponUpdateRequestDto>()
                .ForMember(des => des.CouponId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, CouponStatusRequestDto>().ReverseMap();


            //Garage
            CreateMap<Garage, GarageBookingDto>()
                .ReverseMap().ForMember(des => des.Bookings, obj => obj.Ignore());
            CreateMap<Garage, GarageReviewDto>();
            //----------------------------------------------------------------------------------------------------------------------
            CreateMap<Garage, GarageListResponseDto>()
                .ForMember(des => des.UserGarageDto, obj => obj.MapFrom(src => src.User)).ReverseMap();
            CreateMap<Garage, GarageDetailResponseDto>()
                .ForMember(des => des.UserGarageDto, obj => obj.MapFrom(src => src.User))
                .ForMember(des => des.ReviewGarageDtos, obj => obj.MapFrom(src => src.Reviews))
                .ForMember(des => des.CouponGarageDtos, obj => obj.MapFrom(src => src.Coupons))
                .ForMember(des => des.ServiceGarageGarageDtos, obj => obj.MapFrom(src => src.ServiceGarages)).ReverseMap();
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
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role));
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
            CreateMap<ReviewGarageDto, Review>()
                .ForMember(des => des.Garage, obj => obj.Ignore()).ReverseMap();
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
            CreateMap<ServiceGarage, ServiceGarageGarageDto>()
                .ForMember(des => des.ServiceGarageDto, obj => obj.MapFrom(src => src.Service));
            CreateMap<ServiceGarage, ServiceGarageServiceDto>()
                .ForMember(des => des.GarageServiceDto, obj => obj.MapFrom(src => src.Garage));
            CreateMap<ServiceGarage, ServiceGarageListResponseDto>()
                .ForMember(des => des.ServiceOfServiceGarageDto, obj => obj.MapFrom(src => src.Service));

            //ServiceBooking
            CreateMap<ServiceBooking, ServiceListDto>()
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
                .ForMember(des => des.ServiceGarageServiceDtos, obj => obj.MapFrom(src => src.ServiceGarages))
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
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryProductDto>();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ForMember(des => des.CategoryId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Category, DeleteCategoryDto>().ReverseMap();

            // Product
            CreateMap<Product, ProductServiceDto>().ReverseMap();
            CreateMap<Product, ProductListResponseDto>()
                .ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service))
                .ForMember(des => des.ProductMediaFileProductDtos, obj => obj.MapFrom(src => src.ProductMediaFiles))
                .ReverseMap();
            CreateMap<Product, ProductDetailResponseDto>()
                .ForMember(des => des.SubcategoryProductDto, obj => obj.MapFrom(src => src.Subcategory))
                .ForMember(des => des.ServiceProductDto, obj => obj.MapFrom(src => src.Service))
                .ForMember(des => des.ProductMediaFileProductDtos, obj => obj.MapFrom(src => src.ProductMediaFiles))
                .ReverseMap();
            CreateMap<Product, ProductCreateRequestDto>().ReverseMap();
            CreateMap<Product, ProductPriceRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductStatusRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, ProductQuantityRequestDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();

            //Booking
            CreateMap<Booking, BookingListResponseDto>()
                .ForMember(des => des.CarBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage))
                .ForMember(des => des.ReportBookingDto, obj => obj.MapFrom(src => src.Report))
                .ReverseMap();
            CreateMap<Booking, BookingDetailResponseDto>()
                .ForMember(des => des.CarBookingDto, obj => obj.MapFrom(src => src.Car))
                .ForMember(des => des.GarageBookingDto, obj => obj.MapFrom(src => src.Garage))
                .ForMember(des => des.ReportBookingDto, obj => obj.MapFrom(src => src.Report));
            CreateMap<Booking, BookingCreateRequestDto>()
                .ReverseMap();
            CreateMap<Booking, BookingStatusRequestDto>()
                .ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();

            // ProductMediaFile
            CreateMap<ProductMediaFile, ProductMediaFileProductDto>()
                .ForMember(des => des.MediaFileProductDto, obj => obj.MapFrom(src => src.MediaFile))
                .ReverseMap();

            // MediaFile
            CreateMap<MediaFile, MediaFileProductDto>().ReverseMap();
        }
    }
}