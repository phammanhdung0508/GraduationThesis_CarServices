#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class CreateProductDto
    {
        public string product_name { get; set; }
        public string product_detail_description { get; set; }
        public float product_price { get; set; }
        public int product_quantity { get; set; }
    }
}