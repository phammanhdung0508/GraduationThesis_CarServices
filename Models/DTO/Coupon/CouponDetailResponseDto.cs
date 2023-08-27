namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponDetailResponseDto
    {
        public string CouponCode { get; set; } = string.Empty;
        public string CouponType { get; set; } = string.Empty;
        public string CouponValue { get; set; } = string.Empty;
        public string CouponStartDate { get; set; } = string.Empty;
        public string CouponEndDate { get; set; } = string.Empty;
        public int NumberOfTimesToTUse { get; set; }
        public string CouponStatus { get; set; } = string.Empty;
        public CouponGarageInfoDto? CouponGarageInfoDto { get; set; }
    }

    public class CouponGarageInfoDto
    {
        public string GarageName { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public string GaragePhoneNumber { get; set; } = string.Empty;
        public string GarageAddress { get; set; } = string.Empty;
    }
}