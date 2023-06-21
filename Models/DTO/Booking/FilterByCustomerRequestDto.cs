using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class FilterByCustomerRequestDto
    {
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
        public int UserId {get; set;}
    }
}