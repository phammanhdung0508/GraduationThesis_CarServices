#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserRegisterRequestDto
    {
        public string UserPhone { get; set; }
        public string UserPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }
}