# nullable disable

namespace GraduationThesis_CarServices.Models.DTO.Service
{
    public class ServiceSelectResponseDto
    {
        public string ServiceGroup {get; set;}
        public List<ServicListDto> ServicListDtos {get; set;}
    }
}