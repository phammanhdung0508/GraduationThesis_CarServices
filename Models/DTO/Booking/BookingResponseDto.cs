#nullable disable
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Payment;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Schedule;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingResponseDto
    {
        public DateTime BookingTime { get; set; }
        public BookingStatus BookingStatus { get; set; }

        public CarDto CarDto {get; set;}
        public PaymentDto PaymentDto {get; set;}
        public CouponDto CouponDto {get; set;}
        public ScheduleDto ScheduleDto {get; set;}
        public GarageDto GarageDto {get; set;}
        public ReportDto ReportDto {get; set;}
    }
}