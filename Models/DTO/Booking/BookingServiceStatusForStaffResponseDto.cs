#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingServiceStatusForStaffResponseDto
    {
        public string CarName { get; set; }
        public string Duration { get; set; }
        public string BookingDay { get; set; }
        public string CustomerName { get; set; }
        public string CustomerImage { get; set; }
        public List<MechanicServiceStatusForStaffDto> MechanicServiceStatusForStaffDtos { get; set;}
        public List<ServiceStatusForStaffDto> ServiceStatusForStaffDtos{ get; set;}
    }

    public class MechanicServiceStatusForStaffDto
    {
        public string MechanicName { get; set; }
        public string MechanicImage { get; set; }
    }

    public class ServiceStatusForStaffDto
    {
        public string ServiceName { get; set; }
        public string BookingServiceStatus {get; set;}
    }
}