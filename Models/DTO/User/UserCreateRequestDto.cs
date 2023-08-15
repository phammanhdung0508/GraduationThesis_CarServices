namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserCreateRequestDto
    {
        public int RoleId { get; set; }
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName {get; set;}   = string.Empty;
        public string UserPhone {get; set;} = string.Empty;
        public string? UserEmail { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string PasswordConfirm { get; set; } = string.Empty;
        public string? CarModel { get; set; } = string.Empty;
        public string? CarBrand { get; set; } = string.Empty;
        public string? CarLicensePlate { get; set; } = string.Empty;
        public string? CarDescription { get; set; } = string.Empty;
        public string? CarFuelType { get; set; } = string.Empty;
        public int? NumberOfCarLot {get; set;}
    }
}