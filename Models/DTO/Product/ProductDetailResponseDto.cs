namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductDetailResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImage { get; set; } = string.Empty;
        public string ProductDetailDescription { get; set; } = string.Empty;
        public int ProductWarrantyPeriod { get; set; }
        public string ProductUnit {get; set;} = string.Empty;
        public string ProductPrice { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
        public string ProductStatus { get; set; } = string.Empty;
        public ServiceProductDto? ServiceProductDto { get; set; }
    }
}