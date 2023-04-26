#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class CreateGarageDto
    {
        public string GarageName { get; set; }
        public string GarageContactInformation { get; set; }
        public string GarageAbout { get; set; }
        public string GarageAddress { get; set; }
        public string GarageWard { get; set; }
        public string GarageDistrict { get; set; }        
        public string GarageCity { get; set; }
        public string FromTo { get; set; }
        public string OpenAt { get; set; }
        public string CloseAt { get; set; }
    }
}