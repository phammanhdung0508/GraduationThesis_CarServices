namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailForCustomerResponseDto
    {
        public int GarageId { get; set; }
        public string GaragePhone { get; set; } = string.Empty;
        public string CarName { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CarLicensePlate { get; set; } = string.Empty;
        public string DeviceToken { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string BookingDay { get; set; } = string.Empty;
        public string GarageAddress { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public bool WaitForAccept { get; set; }
        public string DiscountPrice { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public string FinalPrice { get; set; } = string.Empty;
        public List<GroupServiceBookingDetailDto>? GroupServiceBookingDetailDtos { get; set; }
    }

    public class GroupServiceBookingDetailDto
    {
        public string ServiceGroup { get; set; } = string.Empty;
        public List<ServiceListBookingDetailDto>? ServiceListBookingDetailDtos { get; set; }
    }

    public class ServiceListBookingDetailDto
    {
        public bool IsNew { get; set; }
        public string ServicePrice { get; set; } = string.Empty;
        public string ServiceWarranty {get; set;} = string.Empty;
        public string ProductWarranty {get; set;} = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
    }
}