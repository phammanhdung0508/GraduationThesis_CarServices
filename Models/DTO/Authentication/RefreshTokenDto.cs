#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Authentication
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}