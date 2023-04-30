using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserUpdateStatusRequestDto
    {
        public int UserId { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}