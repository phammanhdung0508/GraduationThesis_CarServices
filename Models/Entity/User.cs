#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string UserFirstName { get; set; }
        [MaxLength(20)]
        public string UserLastName { get; set; }
        [MaxLength(50)]
        public string UserEmail { get; set; }
        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] PasswordHash { get; set; }
        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] PasswordSalt { get; set; }
        [MaxLength(30)]
        public string UserAddress { get; set; }
        [MaxLength(20)]
        public string UserCity { get; set; }
        [MaxLength(30)]
        public string UserDistrict { get; set; }
        [MaxLength(40)]
        public string UserWard { get; set; }
        [MaxLength(12)]
        public string UserPhone { get; set; }
        [Column(TypeName = "tinyint")]
        public Gender UserGender { get; set; }
        [Column(TypeName = "date")]
        public Nullable<DateTime> UserDateOfBirth { get; set; }
        [MaxLength(1024)]
        public string UserImage { get; set; }
        [MaxLength(1024)]
        public string UserBio { get; set; }
        [Column(TypeName = "tinyint")]
        public UserStatus UserStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        [MaxLength(1024)]

        /*-------------------------------------------------*/
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}