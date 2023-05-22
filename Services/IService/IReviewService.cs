using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReviewService
    {
        Task<List<ReviewListResponseDto>?> View(PageDto page);
        Task<List<ReviewListResponseDto>?> FilterReviewByGarageId(int garageId, PageDto page);
        Task<ReviewDetailResponseDto?> Detail(int garageId);
        Task<bool> Create(ReviewCreateRequestDto requestDto);
        Task<bool> Update(ReviewUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(ReviewStatusRequestDto requestDto);
    }
}