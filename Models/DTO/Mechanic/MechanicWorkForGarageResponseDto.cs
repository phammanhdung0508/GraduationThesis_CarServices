#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicWorkForGarageResponseDto
    {
        public int MechanicId { get; set; }
        public string Level {get; set;}
        public string FullName {get; set;}
        public string Image {get; set;}
    }
}