#nullable disable
using GraduationThesis_CarServices.Models.DTO.Service;

namespace GraduationThesis_CarServices.Models.DTO.GarageDetail
{
    public class ServiceOfGarageDetailDto
    {
        public int ServiceId { get; set; }        
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public int ServiceDuration { get; set; }
        public ICollection<ProductServiceDto> ProductServiceDtos { get; set; }
        public ICollection<ServiceDetailServiceDto> ServiceDetailServiceDtos { get; set; }

    }
}