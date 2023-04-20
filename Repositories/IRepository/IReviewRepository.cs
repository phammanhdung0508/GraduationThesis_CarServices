using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IReviewRepository
    {
        Task<List<ReviewDto>?> View(PageDto page);
        Task<ReviewDto?> Detail(int id);
        Task Create(CreateReviewDto reviewDto);
        Task Update(UpdateReviewDto reviewDto);
        Task Delete(DeleteReviewDto reviewDto);
    }
}