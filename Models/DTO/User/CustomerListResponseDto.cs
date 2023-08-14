using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class CustomerListResponseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string UserStatus { get; set; } = string.Empty;
        public RoleDto? RoleDto { get; set; }
        public int TotalBooking { get; set; }
    }
}