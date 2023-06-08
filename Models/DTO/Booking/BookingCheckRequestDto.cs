#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCheckRequestDto
    {
        [DefaultValue("06/25/2023")]
        public string DateSelected { get; set; }
        public int GarageId { get; set; }
    }
}