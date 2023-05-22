using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ReviewRepository(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<List<Review>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<Review>
                .Get(context.Reviews
                .Include(r => r.Customer)
                .ThenInclude(c => c.User)
                .Include(r => r.Garage), page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Review>?> FilterGarageReview(int garageId)
        {
            try
            {
                var list = await context.Reviews
                .Where(c => c.GarageId == garageId)
                .Include(r => r.Customer)
                .Include(r => r.Garage)
                .ThenInclude(c => c.User).ToListAsync();

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
    }
}