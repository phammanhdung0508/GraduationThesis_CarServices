#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Authentication
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }
}