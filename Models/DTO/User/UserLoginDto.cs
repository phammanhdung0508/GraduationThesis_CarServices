#nullable disable

using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserLoginDto
    {
        public int UserId { get; set; }
        public string UserToken { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string UserImage { get; set; }
        public string UserPhone { get; set; }
        // public string RefreshToken { get; set; }
        // public DateTime TokenCreated { get; set; }
        // public DateTime TokenExpires { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}