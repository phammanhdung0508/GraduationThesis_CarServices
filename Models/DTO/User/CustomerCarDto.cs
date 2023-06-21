#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class CustomerCarDto
    {
        public string CarBrand { get; set; }
        public string CarLicensePlate { get; set; }
        public string CarDescription { get; set; }
        public string CarFuelType { get; set; }
        public int NumberOfCarLot { get; set; }
    }
}