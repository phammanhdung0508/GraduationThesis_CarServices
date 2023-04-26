#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class CreateServiceDto
    {
        public string ServiceName { get; set; }
        public string ServiceDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public string serviceDuration { get; set; }
    }
}