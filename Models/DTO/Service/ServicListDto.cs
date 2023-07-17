#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServicListDto
    {
        public string ServiceName { get; set; }
        public int ServiceDuration { get; set; }
        public List<ServiceDetailListDto> serviceDetailListDtos {get; set;}
    }
}