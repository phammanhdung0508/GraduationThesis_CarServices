#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class CreateReviewDto
    {
        public int rating { get; set; }
        public string content { get; set; }
    }
}