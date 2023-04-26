#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceBooking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceBookingsId { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float ProductCost { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float ServiceCost { get; set; }

        /*-------------------------------------------------*/
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}