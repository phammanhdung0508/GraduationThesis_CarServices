#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceUpdateRequestDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public int ServiceGroup { get; set; }
        public int ServiceUnit { get; set; }
        public string ServiceDetailDescription { get; set; }
        public int ServiceDuration { get; set; }
    }
}