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

        public async Task<List<ReviewDto>?> View(PageDto page)
        {
            try
            {
                List<Review> list = await PagingConfiguration<Review>.Create(context.Reviews, page);
                return mapper.Map<List<ReviewDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReviewDto?> Detail(int id)
        {
            try
            {
                ReviewDto review = mapper.Map<ReviewDto>(await context.Reviews.FirstOrDefaultAsync(r => r.review_id == id));
                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateReviewDto reviewDto)
        {
            try
            {
                Review Review = mapper.Map<Review>(reviewDto);
                context.Reviews.Add(Review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateReviewDto reviewDto)
        {
            try
            {
                var review = context.Reviews.FirstOrDefault(g => g.review_id == reviewDto.review_id)!;
                mapper.Map<UpdateReviewDto, Review?>(reviewDto, review);
                context.Reviews.Update(review);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteReviewDto reviewDto)
        {
            try
            {
                var review = context.Reviews.FirstOrDefault(g => g.review_id == reviewDto.review_id)!;
                mapper.Map<DeleteReviewDto, Review?>(reviewDto, review);
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