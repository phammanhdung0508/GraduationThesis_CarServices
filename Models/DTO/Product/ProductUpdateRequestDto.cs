#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductUpdateRequestDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDetailDescription { get; set; }
        public float ProductPrice { get; set; }
    }
}