#nullable disable
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class CustomerListResponseDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string UserImage { get; set; }
        public string UserStatus { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}