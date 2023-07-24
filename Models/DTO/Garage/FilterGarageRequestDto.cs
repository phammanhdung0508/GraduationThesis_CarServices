using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class FilterGarageRequestDto
    {
        public int ServiceId { get; set; }
        [DefaultValue("06/25/2023")]
        public string DateSelected { get; set; } = "";
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? RadiusInKm { get; set; }
    }
}