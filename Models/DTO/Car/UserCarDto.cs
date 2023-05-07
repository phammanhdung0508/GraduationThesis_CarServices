#nullable disable
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.Car
{
    public class UserCarDto
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string FullName { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}