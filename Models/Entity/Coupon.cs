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
        [Column(TypeName = "decimal(10,3)")]
        public decimal CouponValue { get; set; }
        // [Column(TypeName = "decimal(10,3)")]
        // public decimal CouponMinSpend { get; set; }
        // [Column(TypeName = "decimal(10,3)")]
        // public decimal CouponMaxSpend { get; set; }
        public int NumberOfTimesToUse { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponStartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime CouponEndDate { get; set; }
        [Column(TypeName = "tinyint")]
        public CouponType CouponType { get; set; }
        [Column(TypeName = "tinyint")]
        public CouponStatus CouponStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /*-------------------------------------------------*/
        public int? GarageId { get; set; }
        public virtual Garage Garage { get; set; }
    }
}