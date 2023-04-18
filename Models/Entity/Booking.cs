#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Booking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int booking_id { get; set; }
        public DateTime booking_time { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime canceled_at { get; set; }
        public DateTime completed_at { get; set; }
        public bool booking_status { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float total_cost { get; set; }

        // public int car_id { get; set; }
        public virtual Car Car { get; set; }
        // public int payment_id { get; set; }
        public virtual Payment Payment { get; set; }
        // public int coupon_id { get; set; }
        public virtual Coupon Coupon { get; set; }
        // public int schedule_id { get; set; }
        public virtual Schedule Schedule { get; set; }
        // public int garage_id { get; set; }
        public virtual Garage Garage { get; set; }
        // public int report_id { get; set; }
        public virtual Report Report { get; set; }

        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
    }
}