#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class SubcategoryProductDto
    {
        public int SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public CategoryProductDto CategoryProductDto { get; set; }
    }
}