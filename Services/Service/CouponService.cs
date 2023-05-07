using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
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

        public async Task<List<CouponListResponseDto>?> FilterGarageCoupon(int garageId)
        {
            try
            {
                var list = mapper
                .Map<List<CouponListResponseDto>>(await couponRepository.FilterGarageCoupon(garageId));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CouponDetailResponseDto?> Detail(int id)
        {
            try
            {
                var coupon = mapper
                .Map<CouponDetailResponseDto>(await couponRepository.Detail(id));

                return coupon;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CouponCreateRequestDto requestDto)
        {
            try
            {
                var coupon = mapper.Map<CouponCreateRequestDto, Coupon>(requestDto,
                otp => otp.AfterMap((src, des) => {
                    des.CreatedAt = DateTime.Now;
                }));
                await couponRepository.Create(coupon);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(CouponUpdateRequestDto requestDto)
        {
            try
            {
                var c = await couponRepository.Detail(requestDto.CouponId);
                var coupon = mapper.Map<CouponUpdateRequestDto, Coupon>(requestDto, c!,
                otp => otp.AfterMap((src, des) => {
                    des.UpdatedAt = DateTime.Now;
                }));
                await couponRepository.Update(coupon);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(CouponStatusRequestDto requestDto)
        {
            try
            {
                var c = await couponRepository.Detail(requestDto.CouponId);
                var coupon = mapper.Map<CouponStatusRequestDto, Coupon>(requestDto, c!);
                await couponRepository.Update(coupon);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}