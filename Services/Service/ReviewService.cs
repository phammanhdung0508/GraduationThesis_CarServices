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
        private readonly IUserService userRepository;
        private readonly IGarageService garageRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IUserService userRepository, IGarageService garageRepository){
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.garageRepository = garageRepository;
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
                ReviewDto? review = await reviewRepository.Detail(id);
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
                await userRepository.Detail(createReviewDto.UserId);
                await garageRepository.Detail(createReviewDto.GarageId);
                
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