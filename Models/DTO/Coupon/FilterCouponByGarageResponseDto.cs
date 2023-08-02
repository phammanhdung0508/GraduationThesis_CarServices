#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class FilterCouponByGarageResponseDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponValue { get; set; }
        public string CouponEndDate { get; set; }
        public string CouponMinSpend { get; set; }
        public string CouponType { get; set; }
    }
}