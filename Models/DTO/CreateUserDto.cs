#nullable disable

namespace CarServices.Models.DTO
{
    public class CreateUserDto
    {
        public string user_email { get; set; }
        public string user_password { get; set; }
    }
}