#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Garage
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int garage_id { get; set; }
        [MaxLength(20)]
        public string garage_name { get; set; }
        [MaxLength(20)]
        public string garage_contact_information { get; set; }
        [MaxLength(100)]
        public string garage_about { get; set; }
        [MaxLength(20)]
        public string garage_address { get; set; }
        [MaxLength(20)]
        public string garage_ward { get; set; }
        [MaxLength(20)]
        public string garage_district { get; set; }
        [MaxLength(20)]
        public string garage_city { get; set; }
        [MaxLength(20)]
        public string from_to { get; set; }
        [MaxLength(6)]
        public string open_at { get; set; }
        [MaxLength(6)]
        public string close_at { get; set; }
        public bool garage_status { get; set; }

        // public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ServiceGarage> ServiceGarages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}