#nullable disable
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserDetailResponseDto
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public string UserDateOfBirth { get; set; }
        public string UserImage { get; set; }
        public string UserBio { get; set; }
        public RoleDto RoleDto { get; set; }
        public UserCustomerDto UserCustomerDto { get; set; }
    }
}