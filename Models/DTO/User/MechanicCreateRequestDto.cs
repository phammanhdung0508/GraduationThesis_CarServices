namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class MechanicCreateRequestDto
    {
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName {get; set;}   = string.Empty;
        public string UserPhone {get; set;} = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
        public int Level {get; set;}

    }
}