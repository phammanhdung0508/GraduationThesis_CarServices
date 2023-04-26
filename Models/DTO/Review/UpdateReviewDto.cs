#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class UpdateReviewDto
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; }
    }
}