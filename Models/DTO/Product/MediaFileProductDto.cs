#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Product
{
    public class MediaFileProductDto
    {
        public int ProductMediaFileId { get; set; }
        public int MediaFileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}