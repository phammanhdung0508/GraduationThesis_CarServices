namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class LocationRequestDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double RadiusInKm { get; set; }
    }
}