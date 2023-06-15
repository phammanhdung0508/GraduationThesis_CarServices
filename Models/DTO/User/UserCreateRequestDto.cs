#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserCreateRequestDto
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string PasswordConfirm { get; set; }
    }
}