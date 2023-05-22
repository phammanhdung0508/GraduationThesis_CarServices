#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class CustomerReviewDto
    {
        public int CustomerId { get; set; }
        public UserReviewDto UserReviewDto { get; set; }
    }
}