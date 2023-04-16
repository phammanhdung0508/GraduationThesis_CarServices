#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int user_id { get; set; }
        [MaxLength(20)]
        public string user_first_name { get; set; }
        [MaxLength(20)]
        public string user_last_name { get; set; }
        [MaxLength(30)]
        public string user_email { get; set; }
        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] password_hash { get; set; }
        [MaxLength(1024)]
        [Required]
        [Column(TypeName = "varbinary(1024)")]
        public byte[] password_salt { get; set; }
        [MaxLength(30)]
        public string user_address { get; set; }
        [MaxLength(20)]
        public string user_city { get; set; }
        [MaxLength(20)]
        public string user_district { get; set; }
        [MaxLength(20)]
        public string user_ward { get; set; }
        [MaxLength(12)]
        public string user_phone { get; set; }
        public bool user_gender { get; set; }
        [Column(TypeName="Date")]
        public DateTime user_date_of_birth { get; set; }
        [MaxLength(1024)]
        public string user_image { get; set; }
        public bool user_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        [MaxLength(1024)]
        public string user_bio { get; set; }

        // public int role_id { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}