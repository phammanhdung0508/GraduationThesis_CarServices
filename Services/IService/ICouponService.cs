using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICouponService
    {
        Task<List<CouponListResponseDto>?> View(PageDto page);
        Task<List<CouponListResponseDto>?> FilterGarageCoupon(int garageId);
        Task<CouponDetailResponseDto?> Detail(int id);
        Task Create(CouponCreateRequestDto requestDto);
        Task Update(CouponUpdateRequestDto requestDto);
        Task UpdateStatus(CouponStatusRequestDto requestDto);
    }
}