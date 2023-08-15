#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailForStaffResponseDto
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress {get; set;}
        public string PickUpTime { get; set; }
        public string DeliveryTime { get; set; }
        public string DiscountPrice { get; set; }
        public string TotalPrice { get; set; }
        public string FinalPrice { get; set; }
        public string BookingStatus { get; set; }
        public CarBookingDetailForStaffDto carBookingDetailForStaffDto {get; set;}
        public List<GroupServiceBookingDetailDto> groupServiceBookingDetailDtos {get; set;}
    }

    public class CarBookingDetailForStaffDto
    {
        public string CarName { get; set; }
        public string CarLicensePlate { get; set; }
    }

    public class GroupServiceBookingDetailForStaffDto
    {
        public string ServiceGroup { get; set; }
        public List<ServiceListBookingDetailDto> serviceListBookingDetailDtos { get; set; }
    }

    public class ServiceListBookingDetailForStaffDto
    {
        public string ServicePrice { get; set; }
        public string ServiceName { get; set; }
    }
}