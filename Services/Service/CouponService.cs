using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CouponService : ICouponService
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly ICouponRepository couponRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMapper mapper;
        public CouponService(ICouponRepository couponRepository, IGarageRepository garageRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.couponRepository = couponRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<GenericObject<List<CouponListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count) = await couponRepository.View(page);

                var listDto = mapper.Map<List<CouponListResponseDto>>(listObj);

                var list = new GenericObject<List<CouponListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<CouponListResponseDto>?> FilterGarageCoupon(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var list = mapper.Map<List<CouponListResponseDto>>(await couponRepository.FilterGarageCoupon(garageId));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<CouponDetailResponseDto?> Detail(int id)
        {
            try
            {
                var coupon = mapper.Map<CouponDetailResponseDto>(await couponRepository.Detail(id));

                switch (false)
                {
                    case var isExist when isExist == (coupon != null):
                        throw new MyException("The coupon doesn't exist.", 404);
                }

                return coupon;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Create(CouponCreateRequestDto requestDto)
        {
            try
            {
                Random random = new Random();
                string code = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray());

                var coupon = mapper.Map<CouponCreateRequestDto, Coupon>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.CouponCode = code;
                    des.CouponStartDate = DateTime.Parse(requestDto.CouponStartDate);
                    des.CouponEndDate = DateTime.Parse(requestDto.CouponEndDate);
                    des.CouponStatus = CouponStatus.Active;
                    des.CreatedAt = DateTime.Now;
                }));

                await couponRepository.Create(coupon);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Update(CouponUpdateRequestDto requestDto)
        {
            try
            {
                var c = await couponRepository.Detail(requestDto.CouponId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The coupon doesn't exist.", 404);
                }

                var coupon = mapper.Map<CouponUpdateRequestDto, Coupon>(requestDto, c!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));

                await couponRepository.Update(coupon);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task UpdateStatus(CouponStatusRequestDto requestDto)
        {
            try
            {
                var c = await couponRepository.Detail(requestDto.CouponId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The coupon doesn't exist.", 404);
                }

                var coupon = mapper.Map<CouponStatusRequestDto, Coupon>(requestDto, c!);

                await couponRepository.Update(coupon);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }
    }
}