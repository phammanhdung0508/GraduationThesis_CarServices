namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceDetailListResponseDto
    {
        public int ServiceDetailId { get; set; }
        public string ServiceDetailDesc { get; set; }  = string.Empty;
        public string ServiceDetailPrice { get; set; } = string.Empty;
    }
}