#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponListResponseDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponStartDate { get; set; }
        public string CouponEndDate { get; set; }
        public string CouponStatus { get; set; }
        public string CreatedAt { get; set; }
    }
}