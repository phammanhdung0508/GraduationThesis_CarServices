namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class GarageDetailResponseDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; } = string.Empty;
        public string GarageImage { get; set; } = string.Empty;
        public string GarageContactInformation { get; set; } = string.Empty;
        public string GarageAbout { get; set; } = string.Empty;
        public string GarageFullAddress {get; set;} = string.Empty;
        public string GarageAddress { get; set; } = string.Empty;
        public string GarageWard { get; set; } = string.Empty;
        public string GarageDistrict { get; set; } = string.Empty;
        public string GarageCity { get; set; } = string.Empty;
        public string HoursOfOperation { get; set; } = string.Empty;
        public string IsOpen { get; set; } = string.Empty;
        public double Rating { get; set; }
        public int AvaliableCoupon {get; set;}

        public byte[]? VersionNumber { get; set; } 
        
        public ManagerGarageDto? ManagerGarageDto { get; set; }
    }
}