#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.Entity
{
    public class Coupon
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CouponId { get; set; }
        [MaxLength(20)]
        public string CouponCode { get; set; }
        [MaxLength(2400)]
        public string CouponDescription { get; set; }
        [MaxLength(20)]
        public CouponType CouponType { get; set; }
        [Range(0, float.MaxValue)]
        public float CouponValue { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponEndDate { get; set; }
        [Range(0, float.MaxValue)]
        public float CouponMinSpend { get; set; }
        [Range(0, float.MaxValue)]
        public float CouponMaxSpend { get; set; }
        [Range(0, int.MaxValue)]
        public int NumberOfTimesToUse { get; set; }
        [Column(TypeName = "tinyint")]
        public CouponStatus CouponStatus { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public Nullable<int> GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}