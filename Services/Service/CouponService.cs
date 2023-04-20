using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        public CouponService(ICouponRepository couponRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.couponRepository = couponRepository;
        }

        public async Task<List<CouponDto>?> View(PageDto page)
        {
            try
            {
                List<CouponDto>? list = await couponRepository.View(page);
                return list;
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
                CouponDto? _coupon = mapper.Map<CouponDto>(await couponRepository.Detail(id));
                return _coupon;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateCouponDto createCouponDto)
        {
            try
            {
                await couponRepository.Create(createCouponDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateCouponDto updateCouponDto)
        {
            try
            {
                await couponRepository.Update(updateCouponDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteCouponDto deleteCouponDto)
        {
            try
            {
                await couponRepository.Delete(deleteCouponDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}