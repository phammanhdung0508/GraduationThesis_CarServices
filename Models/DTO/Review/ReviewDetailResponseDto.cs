#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewDetailResponseDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public Status ReviewStatus { get; set; }
        public CustomerReviewDto CustomerReviewDto { get; set; }
        public GarageReviewDto GarageReviewDto { get; set; }

    }
}