#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class UserReviewDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserImage { get; set; }

        
    }
}