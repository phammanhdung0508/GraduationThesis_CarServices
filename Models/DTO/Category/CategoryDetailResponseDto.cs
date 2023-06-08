#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Category
{
    public class CategoryDetailResponseDto
    {
        public string CategoryName { get; set; }
        public Status CategoryStatus { get; set; }
    }
}