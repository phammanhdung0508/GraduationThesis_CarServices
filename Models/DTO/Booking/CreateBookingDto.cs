#nullable disable
using System.ComponentModel;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Payment;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Schedule;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class CreateBookingDto
    {
        public DateTime BookingTime { get; set; }
        [DefaultValue(BookingStatus.NotStart)]
        public BookingStatus BookingStatus { get; set; }
        
        public int CarId {get; set;}
        public int CouponId { get; set; }
        public int GarageId { get; set; }
    }
}