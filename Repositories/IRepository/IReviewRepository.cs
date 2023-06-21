using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IReviewRepository
    {
        Task<List<Review>?> View(PageDto page);
        Task<List<Review>?> FilterReviewByGarageId(int garageId, PageDto page);
        Task<List<Review>?> FilterAllReview(int? garageId, int? customerId, Status? reviewStatus, DateTime? dateFrom, DateTime? dateTo, PageDto page);
        Task<Review?> Detail(int id);
        Task Create(Review review);
        Task Update(Review review);
    }
}