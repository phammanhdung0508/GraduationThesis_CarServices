namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductUpdateRequestDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; } = string.Empty;
        public string? ProductImage { get; set; } = string.Empty;
        public string? ProductDetailDescription { get; set; } = string.Empty;
        public string? ProductPrice { get; set; } = string.Empty;
        public int? ProductQuantity { get; set; }
        public int? CategoryId { get; set; }
    }
}