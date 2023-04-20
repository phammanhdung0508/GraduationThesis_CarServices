using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReviewService
    {
        Task<List<ReviewDto>?> View(PageDto page);
        Task<ReviewDto?> Detail(int id);
        Task<bool> Create(CreateReviewDto createReviewDto);
        Task<bool> Update(UpdateReviewDto updateReviewDto);
        Task<bool> Delete(DeleteReviewDto deleteReviewDto);
    }
}