using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class DeleteUserDto
    {
        public int user_id { get; set; }
        [DefaultValue("false")]
        public bool user_status { get; set; }
    }
}