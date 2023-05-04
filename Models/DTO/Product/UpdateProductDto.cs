#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class UpdateProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductStatus { get; set; }
    }
}