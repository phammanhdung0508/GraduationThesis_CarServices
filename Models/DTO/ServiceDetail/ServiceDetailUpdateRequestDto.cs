namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceDetailUpdateRequestDto
    {
        public int ServiceDetailId { get; set; }
        public int? MinNumberOfCarLot { get; set; }
        public int? MaxNumberOfCarLot { get; set; }
        public string? ServicePrice { get; set; } = string.Empty;
    }
}