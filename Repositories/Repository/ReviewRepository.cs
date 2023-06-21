using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext context;
        public ReviewRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Review>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Review>
                .Get(context.Reviews.Include(r => r.Garage).Include(r => r.Customer).ThenInclude(r => r.User), page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Review>?> FilterReviewByGarageId(int garageId, PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Review>
                .Get(context.Reviews.Where(c => c.GarageId == garageId), page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Review?> Detail(int id)
        {
            try
            {
                var review = await context.Reviews
                .Where(r => r.ReviewId == id)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Garage)
                .ThenInclude(c => c.User).FirstOrDefaultAsync();

                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Review review)
        {
            try
            {
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Review review)
        {
            try
            {
                context.Reviews.Update(review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Review>?> FilterAllReview(int? garageId, int? customerId, Status? reviewStatus, DateTime? dateFrom, DateTime? dateTo, PageDto page)
        {
            try
            {
                var query = context.Reviews.AsQueryable();

                if (dateFrom == null && dateFrom == null)
                {
                    switch (true)
                    {
                        case var isTrue when isTrue == (garageId > 0 && customerId == null && reviewStatus == null):
                            return await query.Where(r => r.GarageId == garageId).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (customerId > 0 && reviewStatus == null && garageId == null):
                            return await query.Where(r => r.CustomerId == customerId).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (reviewStatus != null && customerId == null && garageId == null):
                            return await query.Where(r => r.ReviewStatus == reviewStatus).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (garageId > 0 && customerId > 0 && reviewStatus == null):
                            return await query.Where(r => r.GarageId == garageId & r.CustomerId == customerId).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (garageId > 0 && reviewStatus != null && customerId == null):
                            return await query.Where(r => r.ReviewStatus == reviewStatus & r.GarageId == garageId).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (customerId > 0 && reviewStatus != null && garageId == null):
                            return await query.Where(r => r.CustomerId == customerId & r.ReviewStatus == reviewStatus).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        default:
                            return await query.Where(r => r.GarageId == garageId & r.CustomerId == customerId & r.ReviewStatus == reviewStatus).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                    }
                }
                else
                {
                    switch (true)
                    {
                        case var isTrue when isTrue == (garageId > 0 && customerId == null && reviewStatus == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.GarageId == garageId & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (customerId > 0 && reviewStatus == null && garageId == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.CustomerId == customerId & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (reviewStatus != null && customerId == null && garageId == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.ReviewStatus == reviewStatus & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (garageId > 0 && customerId > 0 && reviewStatus == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.GarageId == garageId & r.CustomerId == customerId & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (garageId > 0 && reviewStatus != null && customerId == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.ReviewStatus == reviewStatus & r.GarageId == garageId & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        case var isTrue when isTrue == (customerId > 0 && reviewStatus != null && garageId == null && dateFrom != null && dateTo != null):
                            return await query.Where(r => r.CustomerId == customerId & r.ReviewStatus == reviewStatus & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                        default:
                            return await query.Where(r => r.GarageId == garageId & r.CustomerId == customerId & r.ReviewStatus == reviewStatus & dateFrom <= r.CreatedAt & r.CreatedAt <= dateTo).Include(r => r.Garage).Include(r => r.Customer).ThenInclude(c => c.User).ToListAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}