using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class UserDetailMechanicDto
    {
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserGender { get; set; } = string.Empty;
        public RoleDto? RoleDto { get; set; }
    }
}