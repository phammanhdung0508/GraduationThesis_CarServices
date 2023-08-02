#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class UserMechanicDto
    {
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserImage { get; set; }
        public Status UserStatus { get; set; }
    }
}