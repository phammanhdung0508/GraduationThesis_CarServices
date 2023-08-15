namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductCreateRequestDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string ProductDetailDescription { get; set; } = string.Empty;
        public int ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int ServiceId { get; set; }
        public int CategoryId { get; set; }
    }
}