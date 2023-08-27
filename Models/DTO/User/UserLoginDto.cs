using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserLoginDto
    {
        public int UserId { get; set; }
        public int GarageId { get; set; }
        public string UserToken { get; set; } = string.Empty;
        public string? UserFirstName { get; set; } = string.Empty;
        public string? UserLastName { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string? UserImage { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenCreated { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public RoleDto? RoleDto { get; set; }
    }
}