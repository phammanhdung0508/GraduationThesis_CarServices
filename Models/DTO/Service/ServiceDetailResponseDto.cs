#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceDetailResponseDto
    {
        public int ServiceId { get; set; }        
        public string ServiceName { get; set; }
        public string ServiceImage { get; set; }
        public string ServiceDetailDescription { get; set; }
        public int ServiceDuration { get; set; }
        public int ServiceWarrantyPeriod { get; set; }
        public string ServiceStatus {get; set;}

        /*-------------------------------------------------*/
        public ICollection<ProductServiceDto> ProductServiceDtos { get; set; }
        public ICollection<ServiceDetailServiceDto> ServiceDetailServiceDtos { get; set; }
        public ICollection<GarageDetailServiceDto> GarageDetailServiceDtos { get; set; }

    }
}