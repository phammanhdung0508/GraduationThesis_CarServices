#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class CustomerReviewDto
    {
        public int CustomerId { get; set; }
        public UserReviewDto UserReviewDto { get; set; }
    }
}