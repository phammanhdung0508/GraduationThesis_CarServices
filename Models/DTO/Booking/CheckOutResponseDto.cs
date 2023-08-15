namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class CheckOutResponseDto
    {
        public string? OriginalPrice {get; set;}
        public string? DiscountedPrice {get; set;}
        public string? TotalPrice {get; set;}
        public string Deposit {get; set;} = string.Empty;
    }
}