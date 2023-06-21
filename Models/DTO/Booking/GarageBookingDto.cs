#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class GarageBookingDto
    {
        public int GarageId { get; set; }
        public string GarageName { get; set; }
        public string GarageImage { get; set; }
        public string FullAddress {get; set;}
        public string GarageStatus {get; set;}
    }
}