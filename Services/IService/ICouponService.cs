using GraduationThesis_CarServices.Models.DTO.Coupon;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICouponService
    {
        Task<List<CouponListResponseDto>?> FilterGarageCoupon(int garageId);
        Task<CouponDetailResponseDto?> Detail(int id);
        Task<bool> Create(CouponCreateRequestDto requestDto);
        Task<bool> Update(CouponUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(CouponStatusRequestDto requestDto);
    }
}