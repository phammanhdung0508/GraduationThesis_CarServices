#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateRequestDto
    {
        [DefaultValue("04/05/2023")]
        public string DateSelected { get; set; }
        [DefaultValue("7:33:24 AM")]
        public string TimeSelected { get; set; }

        public int CarId { get; set; }
        public int GarageId { get; set; }
        public int CouponId { get; set; }
    }
}