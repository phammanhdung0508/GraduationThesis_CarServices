using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Category
{
    public class CategoryStatusRequestDto
    {
        public int CategoryId { get; set; }
        [DefaultValue("0")]
        public int CategoryStatus { get; set; }
    }
}