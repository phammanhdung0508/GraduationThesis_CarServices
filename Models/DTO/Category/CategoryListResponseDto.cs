#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Category
{
    public class CategoryListResponseDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryStatus { get; set; }
        public int DataCount {get; set;}
    }
}