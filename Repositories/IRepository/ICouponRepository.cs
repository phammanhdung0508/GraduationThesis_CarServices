using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository{
    public interface ICouponRepository{
        Task<List<Coupon>?> FilterGarageCoupon(int garageId);
        Task<Coupon?> Detail(int id);
        Task Create(Coupon coupon);
        Task Update(Coupon coupon);
        Task<bool> IsCouponExist(int couponId);
    }
}