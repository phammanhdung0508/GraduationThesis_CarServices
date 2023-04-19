#nullable disable

using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserLoginDto
    {
        public string user_token { get; set; }
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        public string user_full_name { get; set; }
        public string user_email { get; set; }
        public string user_image { get; set; }
        public string refresh_token { get; set; }
        public DateTime token_created { get; set; }
        public DateTime token_expires { get; set; }
        public RoleDto roleDto { get; set; }
    }
}