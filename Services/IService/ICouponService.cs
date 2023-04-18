using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService{
    public interface ICouponService{
        Task<List<CouponDto>?> View(PageDto page);
        Task<CouponDto?> Detail(int id);
        Task<bool> Create(CreateCouponDto _coupon);
        Task<bool> Update(UpdateCouponDto _coupon);
        Task<bool> Delete(DeleteCouponDto _coupon);
    }
}