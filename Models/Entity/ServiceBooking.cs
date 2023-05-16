#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class ServiceBooking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ServiceBookingId { get; set; }
        [Range(0, float.MaxValue)]
        public float ProductCost { get; set; }
        [Range(0, float.MaxValue)]
        public float ServiceCost { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> BookingId { get; set; }
        public virtual Booking Booking { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public virtual Service Service { get; set; }
        public Nullable<int> ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Nullable<int> MechanicId { get; set; }
        public virtual Mechanic Mechanic { get; set; }
    }
}