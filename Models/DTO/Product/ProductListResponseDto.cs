#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductListResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }

        public double ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductStatus { get; set; }
        public CategoryProductDto CategoryProductDto { get; set; }
    }
}