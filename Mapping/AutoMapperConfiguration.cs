using AutoMapper;
using CarServices.Encrypting;
using CarServices.Models.DTO;
using CarServices.Models.Entity;

namespace CarServices.Mapping
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<User, LoginDto>();
            CreateMap<User, UserDto>().ForMember(des => des.user_full_name,
                obj => obj.MapFrom(src => src.user_first_name + src.user_last_name));
            CreateMap<Role, RoleDto>();
        }
    }
}