using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserListResponseDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string MechanicStatus { get; set; } = string.Empty;
        public string MechanicLevel { get; set; } = string.Empty;
        public string UserStatus { get; set; } = string.Empty;
        public RoleDto? RoleDto { get; set; }
    }
}