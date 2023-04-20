using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Category
{
    public class DeleteCategoryDto
    {
        public int category_id { get; set; }
        [DefaultValue("false")]
        public bool category_status { get; set; }
    }
}