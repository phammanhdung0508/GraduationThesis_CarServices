namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserUpdateRequestDto
    {
        public string UserFirstName { get; set; } = "";
        public string? UserLastName { get; set; }
        public string UserEmail { get; set; } = "";
    }
}