#nullable disable

using System.ComponentModel;

namespace GraduationThesis_CarServices.Models.DTO.Coupon
{
    public class CouponCreateRequestDto
    {
        public float CouponValue { get; set; }
        [DefaultValue("06/25/2023")]
        public string CouponStartDate { get; set; }
        [DefaultValue("06/25/2023")]
        public string CouponEndDate { get; set; }
        public float CouponMinSpend { get; set; }
        public float CouponMaxSpend { get; set; }
        public int NumberOfTimesToUse { get; set; }
        public int CouponType { get; set; }

        public int GarageId { get; set; }
    }
}