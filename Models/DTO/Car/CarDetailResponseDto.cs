#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Car
{
    public class CarDetailResponseDto
    {
        public int CarId { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public string CarLicensePlate { get; set; }
        public string CarFuelType { get; set; }
        public string CarDescription { get; set; }
        public int NumberOfCarLot {get; set;}
    }
}