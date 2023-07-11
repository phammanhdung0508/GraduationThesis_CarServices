using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class ReviewFilterRequestDto
    {
        public Nullable<int> GarageId {get; set;}
        [DefaultValue("06/25/2023")]
        public string? DateFrom {get; set;}
        [DefaultValue("06/25/2023")]
        public string? DateTo {get; set;}
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }
    }
}