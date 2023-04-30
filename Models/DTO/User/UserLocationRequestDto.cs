#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserLocationRequestDto
    {
        public int UserId { get; set; }
        public string UserCity { get; set; }
        public string UserDistrict { get; set; }
        public string UserWard { get; set; }
    }
}