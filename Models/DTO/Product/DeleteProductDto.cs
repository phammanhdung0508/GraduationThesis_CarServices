using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class DeleteProductDto
    {
        public int product_id { get; set; }
        public string product_status { get; set; } = "deleted";
    }
}