#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCheckRequestDto
    {
        [DefaultValue("MM/dd/yyyy")]
        public string DateSelected { get; set; }
        public int TotalEstimatedTimeServicesTake {get; set;}
        public int GarageId { get; set; }
    }
}