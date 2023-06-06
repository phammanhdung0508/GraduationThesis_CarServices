#nullable disable
using System.ComponentModel;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponUpdateRequestDto
    {
        public int CouponId { get; set; }
        public CouponType CouponType { get; set; }
        public float CouponValue { get; set; }
        [DefaultValue("06/25/2023")]
        public string CouponStartDate { get; set; }
        [DefaultValue("06/25/2023")]
        public string CouponEndDate { get; set; }
        public float CouponMinSpend { get; set; }
        public float CouponMaxSpend { get; set; }
        public int NumberOfTimesToTUse { get; set; }
    }
}