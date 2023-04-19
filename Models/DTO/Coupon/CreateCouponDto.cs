#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CreateCouponDto
    {
        public string coupon_code { get; set; }
        public string coupon_type { get; set; }
        public float coupon_value { get; set; }
        public DateTime coupon_start_date { get; set; }
        public DateTime coupon_end_date { get; set; }
        public float coupon_min_spend { get; set; }
        public float coupon_max_spend { get; set; }
        public int number_of_times_to_use { get; set; }
    }
}