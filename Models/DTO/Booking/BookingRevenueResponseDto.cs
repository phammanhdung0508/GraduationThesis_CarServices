namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingRevenueResponseDto
    {
        public decimal AmountEarned {get; set;}
        public decimal ServiceEarned {get; set;}
        public decimal ProductEarned {get; set;}
        public decimal SumPaid {get; set;}
        public decimal SumUnPaid {get; set;}
        public int CountPaid {get; set;}
        public int CountUnpaid {get; set;}
    }
}