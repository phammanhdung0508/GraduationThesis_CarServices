#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class GarageAdminListResponseDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageContactInformation { get; set; }
        public string GarageStatus { get; set; }
        public double Rating { get; set; }
        public int TotalServices {get; set;}
        public int TotalOrders {get; set;}
        public UserGarageDto UserGarageDto {get; set;}
    }
}