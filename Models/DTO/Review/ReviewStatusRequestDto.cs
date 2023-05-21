using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewStatusRequestDto
    {
        public int ReviewId { get; set; }
        public Status ReviewStatus {get; set;}

    }
}