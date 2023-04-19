using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface ICouponRepository{
        Task<List<CouponDto>?> View(PageDto page);
        Task<CouponDto?> Detail(int id);
        Task Create(CreateCouponDto couponDto);
        Task Update(UpdateCouponDto couponDto);
        Task Delete(DeleteCouponDto couponDto);
    }
}