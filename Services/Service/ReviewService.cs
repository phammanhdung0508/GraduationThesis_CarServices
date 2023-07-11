using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IMapper mapper;
        private readonly IReviewRepository reviewRepository;
        private readonly IUserRepository userRepository;
        private readonly IGarageRepository garageRepository;

        public ReviewService(IMapper mapper, IReviewRepository reviewRepository, IUserRepository userRepository, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<GenericObject<List<ReviewListResponseDto>>> View(PageDto page)
        {

            try
            {
                (var listObj, var count) = await reviewRepository.View(page);

                var listDto = mapper.Map<List<ReviewListResponseDto>>(listObj);

                var list = new GenericObject<List<ReviewListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<ReviewListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await reviewRepository.SearchByName(requestDto.Search, page);

                var listDto = mapper.Map<List<ReviewListResponseDto>>(listObj);

                var list = new GenericObject<List<ReviewListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<ReviewListResponseDto>>> FilterReviewByGarage(FilterByGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await reviewRepository.FilterReviewByGarage(requestDto.GarageId, page);

                var listDto = mapper.Map<List<ReviewListResponseDto>>(listObj);

                var list = new GenericObject<List<ReviewListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<ReviewListResponseDto>?> FilterAllReview(ReviewFilterRequestDto requestDto)
        {
            try
            {
                DateTime? dateFrom = null;
                DateTime? dateTo = null;

                if (requestDto.DateFrom is not null)
                {
                    dateFrom = DateTime.Parse(requestDto.DateFrom!);
                }

                if (requestDto.DateTo is not null)
                {
                    dateTo = DateTime.Parse(requestDto.DateTo!);
                }

                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                var list = mapper.Map<List<ReviewListResponseDto>>(await reviewRepository.FilterAllReview(requestDto.GarageId, dateFrom, dateTo, page));

                return list;

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<ReviewDetailResponseDto?> Detail(int reviewId)
        {
            try
            {
                var review = mapper.Map<ReviewDetailResponseDto>(await reviewRepository.Detail(reviewId));

                switch (false)
                {
                    case var isExist when isExist == (review != null):
                        throw new MyException("The review doesn't exist.", 404);
                }

                return review;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Create(ReviewCreateRequestDto requestDto, int userId)
        {
            try
            {
                int customerId = await userRepository.GetCustomerId(userId);

                var isInRange = false;
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                if (requestDto.Rating >= 0 && requestDto.Rating <= 5)
                {
                    isInRange = true;
                }

                switch (false)
                {
                    case var isExist when isExist == (customerId != 0):
                        throw new MyException("The customer doesn't exist.", 404);
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                    case var isRange when isRange == isInRange:
                        throw new MyException("Rating is outside of the range allowed.", 404);
                }

                var review = mapper.Map<ReviewCreateRequestDto, Review>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ReviewStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                    des.CustomerId = customerId;
                }));

                await reviewRepository.Create(review);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Update(ReviewUpdateRequestDto requestDto)
        {
            try
            {
                var r = await reviewRepository.Detail(requestDto.ReviewId);

                switch (false)
                {
                    case var isExist when isExist == (r != null):
                        throw new MyException("The review doesn't exist.", 404);
                    case var isRange when isRange == (requestDto.Rating >= 0 && requestDto.Rating <= 5):
                        throw new MyException("Rating is outside of the range allowed.", 404);
                }

                var review = mapper.Map<ReviewUpdateRequestDto, Review>(requestDto, r!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));

                await reviewRepository.Update(review);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task UpdateStatus(ReviewStatusRequestDto requestDto)
        {
            try
            {
                var r = await reviewRepository.Detail(requestDto.ReviewId);

                switch (false)
                {
                    case var isExist when isExist == (r != null):
                        throw new MyException("The review doesn't exist.", 404);
                }

                var review = mapper.Map<ReviewStatusRequestDto, Review>(requestDto, r!);

                await reviewRepository.Update(review);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }
    }
}