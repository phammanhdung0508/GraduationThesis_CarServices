namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class FilterCouponByGarageResponseDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public string CouponValue { get; set; } = string.Empty;
        public string CouponEndDate { get; set; } = string.Empty;
        public string CouponMinSpend { get; set; } = string.Empty;
        public int NumberOfTimesToUse { get; set; }
        public string CouponType { get; set; } = string.Empty;
    }
}