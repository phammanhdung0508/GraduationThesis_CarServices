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
using GraduationThesis_CarServices.Models.DTO.Payment;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Models.DTO.Subcategory;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.DTO.Booking;

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
            CreateMap<User, UserGarageDto>().ForMember(des => des.FullName,
                obj => obj.MapFrom(src => src.UserFirstName + " " + src.UserLastName))
                .ForMember(des => des.RoleDto, obj => obj.MapFrom(src => src.Role)).ReverseMap();
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


            //Review
            CreateMap<ReviewGarageDto, Review>()
                .ForMember(des => des.Garage, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ForMember(des => des.ReviewId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, DeleteReviewDto>().ReverseMap();


            //ServiceGarage
            CreateMap<ServiceGarageGarageDto, ServiceGarage>()
                .ForMember(des => des.Garage, obj => obj.Ignore())
                .ForMember(des => des.Service, obj => obj.MapFrom(src => src.ServiceGarageDto)).ReverseMap();

            //Service
            CreateMap<ServiceGarageDto, Service>().ReverseMap();
            CreateMap<Service, ServiceDto>().ReverseMap();
            CreateMap<Service, CreateServiceDto>().ReverseMap();
            CreateMap<Service, UpdateServiceDto>().ForMember(des => des.ServiceId, obj => obj.Ignore()).ReverseMap();
            //CreateMap<Service, DeleteServiceDto>().ReverseMap();

            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, CreateReportDto>().ReverseMap();
            CreateMap<Report, UpdateReportDto>().ForMember(des => des.ReportId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Report, DeleteReportDto>().ReverseMap();


            //Car
            CreateMap<Car, CarListResponseDto>().ReverseMap();
            CreateMap<Car, CarDetailResponseDto>().ReverseMap();
            CreateMap<Car, CarCreateRequestDto>().ReverseMap();
            CreateMap<Car, CarUpdateRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Car, CarUpdateRequestDto>()
                .ForMember(des => des.CarId, obj => obj.Ignore()).ReverseMap();

            CreateMap<Payment, PaymentDto>().ReverseMap();
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDto>().ForMember(des => des.PaymentId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Payment, DeletePaymentDto>().ReverseMap();

            CreateMap<Schedule, ScheduleDto>().ReverseMap();
            CreateMap<Schedule, CreateScheduleDto>().ReverseMap();
            CreateMap<Schedule, UpdateScheduleDto>().ForMember(des => des.ScheduleId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Schedule, DeleteScheduleDto>().ReverseMap();

            CreateMap<Subcategory, SubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, CreateSubcategoryDto>().ReverseMap();
            CreateMap<Subcategory, UpdateSubcategoryDto>().ForMember(des => des.SubcategoryId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Subcategory, DeleteSubcategoryDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ForMember(des => des.CategoryId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Category, DeleteCategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ForMember(des => des.ProductId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Product, DeleteProductDto>().ReverseMap();


            //Booking
            CreateMap<Booking, BookingListResponseDto>()
                .ForMember(des => des.ReportDto, obj => obj.MapFrom(src => src.Report))
                .ForMember(des => des.ScheduleDto, obj => obj.MapFrom(src => src.Schedule))
                .ReverseMap();
            CreateMap<Booking, BookingDetailResponseDto>().ReverseMap();
            CreateMap<Booking, BookingCreateRequestDto>()
                .ReverseMap();
            CreateMap<Booking, BookingUpdateRequestDto>()
                .ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();
            CreateMap<Booking, BookingStatusRequestDto>()
                .ForMember(des => des.BookingId, obj => obj.Ignore()).ReverseMap();
        }
    }
}