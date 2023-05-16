#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponDetailResponseDto
    {
        public string CouponCode { get; set; }
        public CouponType CouponType { get; set; }
        public float CouponValue { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
        public float CouponMinSpend { get; set; }
        public float CouponMaxSpend { get; set; }
        public int NumberOfTimesToTUse { get; set; }
        public CouponStatus CouponStatus { get; set; }
    }
}