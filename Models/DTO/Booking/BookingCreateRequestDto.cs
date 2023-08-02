#nullable disable
using System.ComponentModel;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateRequestDto
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        [DefaultValue("MM/dd/yyyy")]
        public string DateSelected { get; set; }
        [DefaultValue("hh:mm:ss")]
        public string TimeSelected { get; set; }
        
        public List<int> ServiceList { get; set; }

        public int MechanicId { get; set; }
        public int CarId { get; set; }
        public int GarageId { get; set; }
        public int CouponId { get; set; }

        //public byte[] VersionNumber { get; set; }
    }
}