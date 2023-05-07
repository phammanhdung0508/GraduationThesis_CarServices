#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponStatusRequestDto
    {
        public int CouponId { get; set; }
        public CouponStatus CouponStatus { get; set; }
    }
}