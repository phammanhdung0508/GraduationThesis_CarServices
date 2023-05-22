#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductCreateRequestDto
    {
        public string ProductName { get; set; }
        public string ProductDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ProductStatus { get; set; }
        public int SubcategoryId { get; set; }
        public int ServiceId { get; set; }
    }
}