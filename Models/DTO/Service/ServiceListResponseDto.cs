namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceListResponseDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceImage { get; set; } = string.Empty;
        public string ServiceGroup {get; set;} = string.Empty;
        public string ServiceUnit {get; set;} = string.Empty;
        public string ServiceStatus {get; set;} = string.Empty;
        public string ServiceDetailDescription { get; set; } = string.Empty;
        public int ServiceDuration { get; set; }
    }
}