using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DataContext context;
        public CouponRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Coupon>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Coupon>.Get(context.Coupons, page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> CountCouponData()
        {
            try
            {
                var count = await context.Coupons.CountAsync();

                return count;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<Coupon>?> FilterGarageCoupon(int garageId)
        {
            try
            {
                var list = await context.Coupons
                .Where(c => c.GarageId == garageId).ToListAsync();
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsCouponExist(int couponId)
        {
            try
            {
                var isExist = await context.Coupons
                .Where(c => c.CouponId == couponId).AnyAsync();

                return isExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Coupon?> Detail(int id)
        {
            try
            {
                var coupon = await context.Coupons
                .FirstOrDefaultAsync(c => c.CouponId == id);

                return coupon;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Coupon coupon)
        {
            try
            {
                context.Coupons.Add(coupon);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Coupon coupon)
        {
            try
            {
                context.Coupons.Update(coupon);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}