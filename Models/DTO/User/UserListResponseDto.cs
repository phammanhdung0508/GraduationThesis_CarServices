#nullable disable
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Role;

namespace GraduationThesis_CarServices.Models.DTO.User
{
    public class UserListResponseDto
    {
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string FullName { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }
        public string UserDistrict { get; set; }
        public string UserWard { get; set; }
        public string UserPhone { get; set; }
        public Gender UserGender { get; set; }
        public Nullable<DateTime> UserDateOfBirth { get; set; }
        public string UserImage { get; set; }
        public string UserBio { get; set; }
        public Status UserStatus { get; set; }
        public RoleDto RoleDto { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
    }
}