#nullable disable

namespace CarServices.Models.DTO
{
    public class UserDto
    {
        public string user_token { get; set;}
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        public string user_full_name { get; set; }
        public string user_email { get; set; }
        public string user_image { get; set; }
        public RoleDto roleDto { get; set; }
    }
}