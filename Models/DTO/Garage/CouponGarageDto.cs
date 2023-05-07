#nullable disable
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class CouponGarageDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public CouponType CouponType { get; set; }
        public DateTime CouponStartDate { get; set; }
        public DateTime CouponEndDate { get; set; }
    }
}