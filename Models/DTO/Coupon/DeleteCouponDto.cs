using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class DeleteCouponDto
    {
        public int CouponId { get; set; }
        [DefaultValue("false")]
        public bool CouponStatus { get; set; }
    }
}