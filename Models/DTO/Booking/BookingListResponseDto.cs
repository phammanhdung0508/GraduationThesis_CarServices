#nullable disable
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.DTO.Schedule;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListResponseDto
    {
        public DateTime BookingTime { get; set; }
        public BookingStatus BookingStatus { get; set; }

        public ScheduleDto ScheduleDto {get; set;}
        // public GarageDto GarageDto {get; set;}
        public ReportDto ReportDto {get; set;}
    }
}