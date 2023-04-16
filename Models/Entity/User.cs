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
        public string user_first_name { get; set; }
        public string user_last_name { get; set; }
        public string user_email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public string user_address { get; set; }
        public string user_phone { get; set; }
        public bool user_gender { get; set; }
        public DateOnly user_date_of_birth { get; set; }
        public string user_image { get; set; }
        public bool user_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string user_bio { get; set; }

        public int role_id { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Garage> Garages { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}