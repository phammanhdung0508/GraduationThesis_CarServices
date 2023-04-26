using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Coupon;
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
        private readonly IMapper mapper;
        public CouponRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<CouponDto>?> View(PageDto page)
        {
            try
            {
                List<Coupon> list = await PagingConfiguration<Coupon>.Create(context.Coupons, page);
                return mapper.Map<List<CouponDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CouponDto?> Detail(int id)
        {
            try
            {
                CouponDto coupon = mapper.Map<CouponDto>(await context.Coupons.FirstOrDefaultAsync(c => c.CouponId == id));
                return coupon;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateCouponDto couponDto)
        {
            try
            {
                Coupon coupon = mapper.Map<Coupon>(couponDto);
                context.Coupons.Add(coupon);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateCouponDto couponDto)
        {
            try
            {
                var coupon = context.Coupons.FirstOrDefault(c => c.CouponId == couponDto.CouponId)!;
                mapper.Map<UpdateCouponDto, Coupon?>(couponDto, coupon);
                context.Coupons.Update(coupon);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteCouponDto couponDto)
        {
            try
            {
                var coupon = context.Coupons.FirstOrDefault(c => c.CouponId == couponDto.CouponId)!;
                mapper.Map<DeleteCouponDto, Coupon?>(couponDto, coupon);
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