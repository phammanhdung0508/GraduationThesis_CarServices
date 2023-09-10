namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class ProductBookingDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCost { get; set; } = string.Empty;
        public string ProductWarranty {get; set;} = string.Empty;
    }
}