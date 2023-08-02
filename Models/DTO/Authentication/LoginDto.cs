using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Authentication
{
    public class LoginDto
    {
        public string? Email {get; set;}
        public string? Phone { get; set; }
        [DefaultValue("abc")]
        public string Password { get; set; } = "";
    }
}