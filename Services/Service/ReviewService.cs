using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IGarageService garageRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IUserRepository userRepository, IGarageService garageRepository)
        {
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<List<ReviewListResponseDto>?> View(PageDto page)
        {

            try
            {
                var list = mapper.Map<List<ReviewListResponseDto>>(await reviewRepository.View(page));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReviewListResponseDto>?> FilterReviewByGarageId(int garageId, PageDto page)
        {
            try
            {
                var list = mapper.Map<List<ReviewListResponseDto>>(await reviewRepository.FilterReviewByGarageId(garageId, page));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReviewDetailResponseDto?> Detail(int reviewId)
        {
            try
            {
                switch (await reviewRepository.IsReviewExist(reviewId))
                {
                    case true:
                        var review = mapper.Map<ReviewDetailResponseDto>(await reviewRepository.Detail(reviewId));
                        return review;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(ReviewCreateRequestDto requestDto)
        {
            try
            {
                switch (await userRepository.IsCustomer(requestDto.CustomerId))
                {
                    case true:
                        var review = mapper.Map<ReviewCreateRequestDto, Review>(requestDto,
                        otp => otp.AfterMap((src, des) =>
                        {
                            des.ReviewStatus = Status.Activate;
                            des.CreatedAt = DateTime.Now;
                        }));
                        await reviewRepository.Create(review);
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ReviewUpdateRequestDto requestDto)
        {
            try
            {
                switch (await reviewRepository.IsReviewExist(requestDto.ReviewId))
                {
                    case true:
                        var r = await reviewRepository.Detail(requestDto.ReviewId);
                        var review = mapper.Map<ReviewUpdateRequestDto, Review>(requestDto, r!,
                        otp => otp.AfterMap((src, des) =>
                        {
                            des.UpdatedAt = DateTime.Now;
                        }));
                        await reviewRepository.Update(review);
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(ReviewStatusRequestDto requestDto)
        {
            try
            {
                var r = await reviewRepository.Detail(requestDto.ReviewId);
                var review = mapper.Map<ReviewStatusRequestDto, Review>(requestDto, r!);
                await reviewRepository.Update(review);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}