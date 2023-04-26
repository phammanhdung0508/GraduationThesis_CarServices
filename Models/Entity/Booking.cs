#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Booking
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }
        public DateTime BookingTime { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> CanceledAt { get; set; }
        [Column(TypeName = "tinyint")]
        public BookingStatus BookingStatus { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float TotalCost { get; set; }
        
        /*-------------------------------------------------*/
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public int CouponId { get; set; }
        public virtual Coupon Coupon { get; set; }
        public int ScheduleId { get; set; }
        public virtual Schedule Schedule { get; set; }
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public int ReportId { get; set; }
        public virtual Report Report { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<ServiceBooking> ServiceBookings { get; set; }
    }
}