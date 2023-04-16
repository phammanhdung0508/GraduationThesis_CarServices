#nullable disable

namespace CarServices.Models.DTO
{
    public class RefreshTokenDto
    {
        public string Token { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Expires { get; set; }
    }
}