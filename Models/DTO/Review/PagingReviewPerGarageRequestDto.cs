#nullable disable
using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Review
{
    public class PagingReviewPerGarageRequestDto
    {
        [DefaultValue(1)]
        public int PageIndex { get; set; }
        [DefaultValue(10)]
        public int PageSize { get; set; }

        public int GarageId { get; set; }
    }
}