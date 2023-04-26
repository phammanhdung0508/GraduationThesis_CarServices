#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class UpdateServiceDto
    {
        public int ServiceId { get; set; }        
        public string ServiceName { get; set; }
        public string ServiceDetailDescription { get; set; }
        public float ProductPrice { get; set; }
        public string serviceDuration { get; set; }
    }
}