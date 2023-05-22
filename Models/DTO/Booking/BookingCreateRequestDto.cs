#nullable disable
using System.ComponentModel;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateRequestDto
    {
        [DefaultValue("04/05/2023")]
        public string DateSelected { get; set; }
        [DefaultValue("8:00:00")]
        public string TimeSelected { get; set; }
        public string PaymentMethod { get; set; }
        [DefaultValue(2)]
        public PaymentStatus PaymentStatus { get; set; }
        public List<ServiceListDto> ServiceList { get; set; }

        public byte[] VersionNumber { get; set; }

        public int CarId { get; set; }
        public int GarageId { get; set; }
        public int CouponId { get; set; }
    }
}