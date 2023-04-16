#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarServices.Models.Entity
{
    public class ServiceBooking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int service_bookings_id { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float product_cost { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float service_cost { get; set; }

        // public int booking_id { get; set; }
        public virtual Booking Booking { get; set; }
        // public int service_id { get; set; }
        public virtual Service Service { get; set; }

    }
}