using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Category
{
    public class DeleteCategoryDto
    {
        public int CategoryId { get; set; }
        [DefaultValue("false")]
        public bool CategoryStatus { get; set; }
    }
}