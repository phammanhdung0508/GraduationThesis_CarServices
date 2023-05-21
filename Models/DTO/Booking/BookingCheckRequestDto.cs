#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class BookingCheckRequestDto
    {
        [DefaultValue("04/05/2023")]
        public string DateSelected { get; set; }
        public int GarageId { get; set; }
    }
}