namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponListResponseDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public int NumberOfTimesToUse { get; set; }
        public string CouponStartDate { get; set; } = string.Empty;
        public string CouponEndDate { get; set; } = string.Empty;
        public string CouponStatus { get; set; } = string.Empty;
        public string GarageName { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}