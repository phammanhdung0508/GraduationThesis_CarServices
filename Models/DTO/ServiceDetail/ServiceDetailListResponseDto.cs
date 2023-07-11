#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceDetailListResponseDto
    {
        public int ServiceDetailId { get; set; }
        public double ServicePrice { get; set; }
        public ServiceOfServiceDetailDto ServiceOfServiceDetailDto { get; set; }

    }
}