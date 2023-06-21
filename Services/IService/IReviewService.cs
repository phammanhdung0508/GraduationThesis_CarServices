using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReviewService
    {
        Task<List<ReviewListResponseDto>?> View(PageDto page);
        Task<List<ReviewListResponseDto>?> FilterReviewByGarageId(PagingReviewPerGarageRequestDto requestDto);
        Task<ReviewDetailResponseDto?> Detail(int garageId);
        Task Create(ReviewCreateRequestDto requestDto, int userId);
        Task Update(ReviewUpdateRequestDto requestDto);
        Task UpdateStatus(ReviewStatusRequestDto requestDto);
        Task<List<ReviewListResponseDto>?> FilterAllReview(ReviewFilterRequestDto requestDto);
    }
}