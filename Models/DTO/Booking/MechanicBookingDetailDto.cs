namespace GraduationThesis_CarServices.Models.DTO.Booking
{
    public class MechanicBookingDetailDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
    }
}