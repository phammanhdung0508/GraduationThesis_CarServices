#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductListResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public SubcategoryProductDto SubcategoryProductDto { get; set; }
        public ServiceProductDto ServiceProductDto { get; set; }
    }
}