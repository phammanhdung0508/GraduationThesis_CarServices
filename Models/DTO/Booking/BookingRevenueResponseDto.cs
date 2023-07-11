namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingRevenueResponseDto
    {
        public double AmountEarned {get; set;}
        public double ServiceEarned {get; set;}
        public double ProductEarned {get; set;}
        public double SumPaid {get; set;}
        public double SumUnPaid {get; set;}
        public int CountPaid {get; set;}
        public int CountUnpaid {get; set;}
    }
}