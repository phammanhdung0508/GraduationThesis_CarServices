#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewCreateRequestDto
    {
        public int Rating { get; set; }
        public string Content { get; set; }

        public int GarageId { get; set; }
    }
}