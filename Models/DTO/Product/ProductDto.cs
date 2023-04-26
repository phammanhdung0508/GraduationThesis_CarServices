#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string ProductDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductSold { get; set; }
    }
}