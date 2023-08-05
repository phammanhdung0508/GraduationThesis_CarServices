#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingDetailForCustomerResponseDto
    {
        public string CarName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Duration { get; set; }
        public string BookingDay { get; set; }
        public string GarageAddress { get; set; }
        public string BookingStatus { get; set; }
        public string QrImage { get; set; }
        public string DiscountPrice { get; set; }
        public string TotalPrice { get; set; }
        public string FinalPrice { get; set; }
        public List<GroupServiceBookingDetailDto> groupServiceBookingDetailDtos {get; set;}
    }

    public class GroupServiceBookingDetailDto
    {
        public string ServiceGroup { get; set; }
        public List<ServiceListBookingDetailDto> serviceListBookingDetailDtos {get; set;}
    }

    public class ServiceListBookingDetailDto
    {
        public string ServicePrice { get; set; }
        public string ServiceName { get; set; }
    }
}