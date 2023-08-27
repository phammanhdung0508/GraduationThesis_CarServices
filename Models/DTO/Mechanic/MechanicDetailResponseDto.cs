namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class MechanicDetailResponseDto
    {
        public int MechanicId { get; set; }
        public int TotalBookingApplied { get; set; }
        public string IsAvaliable { get; set; } = string.Empty;

        public UserDetailMechanicDto? UserDetailMechanicDto { get; set; }
        public BookingMechanicCurrentWorkingOn? BookingMechanicCurrentWorkingOn { get; set; }
    }

    public class BookingMechanicCurrentWorkingOn
    {
        public int BookingId { get; set; }
        public string BookingCode { get; set; } = string.Empty;
        public string BookingTime { get; set; } = string.Empty;
        public string CarInfo { get; set; } = string.Empty;
        public string GarageName { get; set; } = string.Empty;
    }
}