#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class Car
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int car_id { get; set; }
        [MaxLength(20)]
        public string car_model { get; set; }
        [MaxLength(20)]
        public string car_brand { get; set; }
        [MaxLength(5)]
        public string car_license_plate { get; set; }
        [Column(TypeName="Date")]
        public DateTime car_year { get; set; }
        [MaxLength(20)]
        public string car_body_type { get; set; }
        [MaxLength(20)]
        public string car_fuel_type { get; set; }

        // public int user_id { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}