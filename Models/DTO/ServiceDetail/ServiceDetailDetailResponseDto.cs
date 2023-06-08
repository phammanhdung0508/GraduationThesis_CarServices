#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.ServiceDetail
{
    public class ServiceDetailDetailResponseDto
    {
        public int ServiceDetailId { get; set; }
        public float ServicePrice { get; set; }
        public int MinNumberOfCarLot { get; set; }
        public int MaxNumberOfCarLot { get; set; }
        public ServiceOfServiceDetailDto ServiceOfServiceDetailDto { get; set; }

    }
}