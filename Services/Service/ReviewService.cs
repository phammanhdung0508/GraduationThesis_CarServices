using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;
        public ReviewService(IMapper mapper, IReviewRepository reviewRepository){
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }

        public async Task<List<ReviewDto>?> View(PageDto page)
        {

            try
            {
                List<ReviewDto>? list = await reviewRepository.View(page);
                return list;
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
                ReviewDto? review = mapper.Map<ReviewDto>(await reviewRepository.Detail(id));
                return review;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateReviewDto createReviewDto)
        {
            try
            {
                await reviewRepository.Create(createReviewDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateReviewDto updateReviewDto)
        {
            try
            {
                await reviewRepository.Update(updateReviewDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteReviewDto deleteReviewDto)
        {
            try
            {
                await reviewRepository.Delete(deleteReviewDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}