namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceOfServiceDetailDto
    {
        public int ServiceId { get; set; }
        public string ServiceImage { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceGroup {get; set;} = string.Empty;
        public int ServiceWarrantyPeriod { get; set; }
        public string ServiceUnit {get; set;} = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public string ServiceStatus { get; set; } = string.Empty;
        public List<ServiceDetailListResponseDto>? ServiceDetailListResponseDtos { get; set; }
    }
}