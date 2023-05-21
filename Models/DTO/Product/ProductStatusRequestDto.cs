#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class ProductStatusRequestDto
    {
        public int ProductId { get; set; }
        public Status ProductStatus { get; set; }
    }
}