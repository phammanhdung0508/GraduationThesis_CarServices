#nullable disable

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserUpdateRequestDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
    }
}