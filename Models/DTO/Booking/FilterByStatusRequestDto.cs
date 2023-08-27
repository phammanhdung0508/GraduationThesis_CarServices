using System.ComponentModel;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class FilterByStatusRequestDto
    {
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public int? GarageId { get; set; }
    }
}