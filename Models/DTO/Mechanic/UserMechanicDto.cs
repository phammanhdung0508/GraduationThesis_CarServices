namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class UserMechanicDto
    {
        public string FullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public int UserStatus { get; set; }
    }
}