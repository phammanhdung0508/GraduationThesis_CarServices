using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCreateForManagerRequestDto
    {
        public string CustomerName { get; set; } = "N/A";
        public string CustomerPhone { get; set; } = "N/A";
        public string CustomerEmail { get; set; } = "N/A";

        [DefaultValue("MM/dd/yyyy")]
        public string DateSelected { get; set; } = string.Empty;
        [DefaultValue("hh:mm:ss")]
        public string TimeSelected { get; set; } = string.Empty;

        public List<ServiceList>? ServiceList { get; set; }
        
        public int CarId { get; set; }
        public int GarageId { get; set; }
    }

    public class ServiceList
    {
        public int ServiceDetailId {get; set;}
        public int ProductId {get; set;}
    }
}