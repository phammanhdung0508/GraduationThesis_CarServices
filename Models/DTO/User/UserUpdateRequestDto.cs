#nullable disable

using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserUpdateRequestDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserCity { get; set; }
        public string UserDistrict { get; set; }
        public string UserWard { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public Gender UserGender { get; set; }
        public DateTime UserDateOfBirth { get; set; }
        public string UserBio { get; set; }
    }
}