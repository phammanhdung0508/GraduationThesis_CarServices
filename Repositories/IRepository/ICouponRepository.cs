using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface ICouponRepository{
        Task<List<Coupon>?> View(PageDto page);
        Task<List<Coupon>?> FilterGarageCoupon(int garageId);
        Task<Coupon?> Detail(int id);
        Task Create(Coupon coupon);
        Task Update(Coupon coupon);
        Task<bool> IsCouponExist(int couponId);
        Task<int> CountCouponData();
    }
}