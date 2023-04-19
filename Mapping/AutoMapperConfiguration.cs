using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Role;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Review;

namespace GraduationThesis_CarServices.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, LoginDto>();
            CreateMap<User, UserLoginDto>().ForMember(des => des.user_full_name,
                obj => obj.MapFrom(src => src.user_first_name + src.user_last_name))
                .ForMember(des => des.roleDto, obj => obj.MapFrom(src => src.Role));
                
            CreateMap<Role, RoleDto>();

            CreateMap<Coupon, CouponDto>().ReverseMap();
            CreateMap<Coupon, CreateCouponDto>().ReverseMap();
            CreateMap<Coupon, UpdateCouponDto>().ForMember(des => des.coupon_id, obj => obj.Ignore()).ReverseMap();
            CreateMap<Coupon, DeleteCouponDto>().ReverseMap();

            CreateMap<Garage, GarageDto>().ReverseMap();
            CreateMap<Garage, CreateGarageDto>().ReverseMap();
            CreateMap<Garage, UpdateGarageDto>().ForMember(des => des.garage_id, obj => obj.Ignore()).ReverseMap();
            CreateMap<Garage, DeleteGarageDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ForMember(des => des.user_id, obj => obj.Ignore()).ReverseMap();
            CreateMap<User, DeleteUserDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ForMember(des => des.review_id, obj => obj.Ignore()).ReverseMap();
            CreateMap<Review, DeleteReviewDto>().ReverseMap();

            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Report, CreateReportDto>().ReverseMap();
            CreateMap<Report, UpdateReportDto>().ForMember(des => des.report_id, obj => obj.Ignore()).ReverseMap();
            CreateMap<Report, DeleteReportDto>().ReverseMap();
        }
    }
}