#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class GarageUpdateRequestDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageContactInformation { get; set; }
        public string GarageAbout { get; set; }
        public string OpenAt { get; set; }
        public string CloseAt { get; set; }
    }
}