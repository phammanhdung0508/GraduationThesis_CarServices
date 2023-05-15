using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class ReportBookingDto
    {
        public int ReportId { get; set; }
        public DateTime Date { get; set; }
        public Status ReportStatus { get; set; }
    }
}