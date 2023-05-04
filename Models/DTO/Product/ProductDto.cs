#nullable disable
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.DTO.Subcategory;

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string ProductDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductStatus { get; set; }
        public SubcategoryDto SubcategoryDto { get; set; }
        public ServiceDto ServiceDto { get; set; }
    }
}