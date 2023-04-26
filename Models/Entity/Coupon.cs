#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Coupon
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CouponId { get; set; }
        [MaxLength(20)]
        public string CouponCode { get; set; }
        [MaxLength(20)]
        public string CouponType { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float CouponValue { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponEndDate { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float CouponMinSpend { get; set; }
        [Range(0, float.MaxValue, ErrorMessage = "")]
        public float CouponMaxSpend { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "")]
        public int NumberOfTimesToUse { get; set; }
        [Column(TypeName = "tinyint")]
        public int CouponStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}