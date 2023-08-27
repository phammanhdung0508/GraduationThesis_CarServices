namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingRevenueResponseDto
    {
        public string AmountEarned { get; set; } = "0 VND";
        public string ServiceEarned { get; set; } = "0 VND";
        public string ProductEarned { get; set; } = "0 VND";
        public string SumPaid { get; set; } = "0 VND";
        public string SumUnPaid { get; set; } = "0 VND";
        public int CountPaid { get; set; }
        public int CountUnpaid { get; set; }
        public int CountCheckInt { get; set; }
        public int CountCheckOut { get; set; }
    }
}