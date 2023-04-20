#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewDto
    {
        public int rating { get; set; }
        public string content { get; set; }
        public bool is_approved { get; set; }
    }
}