namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class CountBookingPerStatusDto
    {
        public int Pending {get; set;}
        public int Canceled {get; set;}
        public int CheckIn {get; set;}
        public int Processing {get; set;}
        public int Completed {get; set;}
    }
}