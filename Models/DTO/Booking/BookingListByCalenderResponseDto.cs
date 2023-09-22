namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingListByCalenderResponseDto
    {
        public int Id { get; set; }
        public bool AllDay { get; set; } = false;
        public string Title { get; set; } = string.Empty;
        public string Start { get; set; } = string.Empty;
        public string End { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
    }
}