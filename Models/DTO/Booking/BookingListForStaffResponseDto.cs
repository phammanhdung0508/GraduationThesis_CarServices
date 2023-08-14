#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class HourDto
    {
        public string Hour { get; set; }
        public List<BookingListForStaffResponseDto> BookingListForStaffResponseDtos { get; set; }
    }

    public class BookingListForStaffResponseDto
    {
        public int BookingId { get; set; }
        public string CarName { get; set; }
        public string CustomerName { get; set; }
        public string BookingDuration { get; set; }
        public string BookingStatus { get; set; }
    }
}