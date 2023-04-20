#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class UpdateReviewDto
    {
        public int review_id { get; set; }
        public int rating { get; set; }
        public string content { get; set; }
        public bool is_approved { get; set; }
    }
}