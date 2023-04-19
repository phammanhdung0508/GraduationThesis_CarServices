using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class DeleteCouponDto
    {
        public int coupon_id { get; set; }
        [DefaultValue("false")]
        public bool coupon_status { get; set; }
    }
}