namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCountResponseDto
    {
        public int Pending { get; set; }
        public int Canceled { get; set; }
        public int CheckIn { get; set; }
        public int Completed { get; set; }
        public int Warranty { get; set; }
    }
}