using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class DeleteProductDto
    {
        public int ProductId { get; set; }
        public string ProductStatus { get; set; } = "deleted";
    }
}