#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicWorkForBookingResponseDto
    {
        public int MechanicId { get; set; }
        public bool IsMainMechanic {get; set;} = false; 
        public string FullName {get; set;}
        public string Image {get; set;}
    }
}