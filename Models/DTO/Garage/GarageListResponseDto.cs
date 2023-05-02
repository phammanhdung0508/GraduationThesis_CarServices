#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class GarageListResponseDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageImage { get; set; }
        public string GarageAddress { get; set; }
        public string GarageWard { get; set; }
        public string GarageDistrict { get; set; }
        public string GarageCity { get; set; }
        public int GarageStatus { get; set; }

        public UserGarageDto UserGarageDto {get; set;}
    }
}