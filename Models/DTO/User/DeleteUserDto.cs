using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class DeleteUserDto
    {
        public int UserId { get; set; }
        [DefaultValue("false")]
        public bool UserStatus { get; set; }
    }
}