#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class FilterBookingByMechanicRequestDto
    {
        public int UserId { get; set; }        
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}