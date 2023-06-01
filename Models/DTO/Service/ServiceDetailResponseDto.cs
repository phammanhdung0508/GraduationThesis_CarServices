#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceDetailResponseDto
    {
        public int ServiceId { get; set; }        
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceDetailDescription { get; set; }
        public float ServicePrice { get; set; }
        public int ServiceDuration { get; set; }
        public Status ServiceStatus {get; set;}

        /*-------------------------------------------------*/
        public ICollection<ProductServiceDto> ProductServiceDtos { get; set; }
        public ICollection<ServiceGarageServiceDto> ServiceGarageServiceDtos { get; set; }
    }
}