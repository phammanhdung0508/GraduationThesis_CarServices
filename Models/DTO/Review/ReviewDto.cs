#nullable disable
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.DTO.Garage;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public string Content { get; set; }
        public bool IsApproved { get; set; }

        public UserDto UserDto { get; set; }
        public GarageDto GarageDto { get; set; }
    }
}