namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceDetailServiceDto
    {
        public int ServiceDetailId { get; set; }
        public string ServicePrice { get; set; } = string.Empty;
        public string MinNumberOfCarLot { get; set; } = string.Empty;
        public string MaxNumberOfCarLot { get; set; } = string.Empty;
    }
}