#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceStatusRequestDto
    {
        public int ServiceId { get; set; }        
        public Status ServiceStatus {get; set;}
    }
}