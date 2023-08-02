using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReviewService
    {
        Task<GenericObject<List<ReviewListResponseDto>>> View(PageDto page);
        Task<GenericObject<List<ReviewListResponseDto>>> FilterReviewByGarage(FilterByGarageRequestDto requestDto);
        Task<ReviewDetailResponseDto?> Detail(int garageId);
        Task Create(ReviewCreateRequestDto requestDto, int userId);
        Task Update(ReviewUpdateRequestDto requestDto);
        Task UpdateStatus(ReviewStatusRequestDto requestDto);
        Task<List<ReviewListResponseDto>?> FilterAllReview(ReviewFilterRequestDto requestDto);
        Task<GenericObject<List<ReviewListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto);
    }
}