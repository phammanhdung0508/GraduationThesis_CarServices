#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class CustomerBookingDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserImage { get; set; }
    }
}