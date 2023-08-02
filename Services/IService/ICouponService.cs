using GraduationThesis_CarServices.Models.DTO.Coupon;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface ICouponService
    {
        Task<GenericObject<List<CouponListResponseDto>>> View(PageDto page);
        Task<List<FilterCouponByGarageResponseDto>?> FilterGarageCoupon(int garageId);
        Task<CouponDetailResponseDto?> Detail(int id);
        Task Create(CouponCreateRequestDto requestDto);
        Task Update(CouponUpdateRequestDto requestDto);
        Task UpdateStatus(CouponStatusRequestDto requestDto);
    }
}