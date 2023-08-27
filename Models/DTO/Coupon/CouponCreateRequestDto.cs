using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponCreateRequestDto
    {
        public decimal CouponValue { get; set; }
        [DefaultValue("06/25/2023")]
        public string CouponStartDate { get; set; } = string.Empty;
        [DefaultValue("06/25/2023")]
        public string CouponEndDate { get; set; } = string.Empty;
        public int NumberOfTimesToUse { get; set; }
        public int CouponType { get; set; }
        public int GarageId { get; set; }
    }
}