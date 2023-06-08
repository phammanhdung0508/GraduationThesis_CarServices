#nullable disable
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.Mechanic
{
    public class UserDetailMechanicDto
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string FullName { get; set; }
        public string UserImage { get; set; }
        public string UserPhone { get; set; }
        public string UserGender { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}