#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewListResponseDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string CreatedAt {get; set;}
        public string ReviewStatus {get; set;}
        public UserReviewDto UserReviewDto {get; set;}
        public GarageReviewDto GarageReviewDto {get; set;}
    }
}