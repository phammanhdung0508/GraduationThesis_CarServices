#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewDetailResponseDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public CustomerReviewDto CustomerReviewDto { get; set; }
        public GarageReviewDto GarageReviewDto { get; set; }

    }
}