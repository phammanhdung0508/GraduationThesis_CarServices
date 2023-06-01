#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicListResponseDto
    {
        public int MechanicId { get; set; }
        public int TotalWorkingHours { get; set; }

        public UserMechanicDto UserMechanicDto { get; set; }
    }
}