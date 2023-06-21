#nullable disable
namespace GraduationThesis_CarServices.Models.DTO.Garage
{
    public class CouponGarageDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
        public string CouponEndDate { get; set; }
        public string TotalService {get; set;}
        public string TotalProduct {get; set;}
        public string TotalOrders {get; set;}
        public GarageManagerDto GarageManagerDto {get; set;}
    }
}