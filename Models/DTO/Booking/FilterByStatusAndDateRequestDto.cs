using System.ComponentModel;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class FilterByStatusAndDateRequestDto
    {
        public BookingStatus? BookingStatus { get; set; }
        [DefaultValue("06/25/2023")]
        public string? DateFrom { get; set; }
        [DefaultValue("06/25/2023")]
        public string? DateTo { get; set; }
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}