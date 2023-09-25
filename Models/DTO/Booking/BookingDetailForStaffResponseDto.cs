namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailForStaffResponseDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress {get; set;} = string.Empty;
        public bool WaitForAccept { get; set; }
        public string DeviceToken { get; set; } = string.Empty;
        public string PickUpTime { get; set; } = string.Empty;
        public string DeliveryTime { get; set; } = string.Empty;
        public string DiscountPrice { get; set; } = string.Empty;
        public string TotalPrice { get; set; } = string.Empty;
        public string FinalPrice { get; set; } = string.Empty;
        public string BookingStatus { get; set; } = string.Empty;
        public CarBookingDetailForStaffDto? carBookingDetailForStaffDto {get; set;}
        public List<GroupServiceBookingDetailDto>? groupServiceBookingDetailDtos {get; set;}
    }

    public class CarBookingDetailForStaffDto
    {
        public string CarName { get; set; } = string.Empty;
        public string CarLicensePlate { get; set; } = string.Empty;
    }

    public class GroupServiceBookingDetailForStaffDto
    {
        public string ServiceGroup { get; set; } = string.Empty;
        public List<ServiceListBookingDetailDto>? serviceListBookingDetailDtos { get; set; }
    }

    public class ServiceListBookingDetailForStaffDto
    {
        public string ServicePrice { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
    }
}