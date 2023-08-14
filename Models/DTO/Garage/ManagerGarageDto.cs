namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class ManagerGarageDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserStatus { get; set; } = string.Empty;
    }
}