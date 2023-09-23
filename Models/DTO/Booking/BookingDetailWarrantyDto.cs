namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailWarrantyDto
    {
        public string GarageName { get; set; } = string.Empty;
        public string GarageImage { get; set; } = string.Empty;
        public string FullAddress { get; set; } = string.Empty;
        public string GarageStatus { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public List<BookingDetailList>? BookingDetailLists { get; set; }
    }

    public class BookingDetailList
    {
        public int BookingId { get; set; }
        public bool WaitForAccept { get; set; }
        public string BookingTime { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string OriginalPrice { get; set; } = string.Empty;
        public string DiscountPrice { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public string FinalPrice { get; set; } = string.Empty;
        public List<BookingDetailDto>? BookingDetailDtos { get; set; }
    }
}