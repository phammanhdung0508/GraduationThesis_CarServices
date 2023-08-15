namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicDetailResponseDto
    {
        public int MechanicId { get; set; }
        public int TotalBookingApplied { get; set; }

        public UserDetailMechanicDto? UserDetailMechanicDto { get; set; }
    }
}