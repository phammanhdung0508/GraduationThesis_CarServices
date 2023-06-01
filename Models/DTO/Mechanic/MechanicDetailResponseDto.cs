#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicDetailResponseDto
    {
        public int MechanicId { get; set; }
        public int TotalWorkingHours { get; set; }
        public string Specialities { get; set; }

        public UserDetailMechanicDto UserDetailMechanicDto { get; set; }
    }
}