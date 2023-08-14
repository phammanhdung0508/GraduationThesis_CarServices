#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicListResponseDto
    {
        public int UserId { get; set; }
        public string Level {get; set;}
        public int TotalOrders {get; set;}

        public UserMechanicDto UserMechanicDto { get; set; }
    }
}