using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-review")]
        public async Task<IActionResult> ViewAllReview(PageDto page)
        {
            var list = await reviewService.View(page)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("get-garage-reviews")]
        public async Task<IActionResult> GerReviewPerGarage(PagingReviewPerGarageRequestDto requestDto)
        {
            var list = await reviewService.FilterReviewByGarageId(requestDto)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-review/{id}")]
        public async Task<IActionResult> DetailReview(int id)
        {
            var review = await reviewService.Detail(id);
            return Ok(review);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create-review")]
        public async Task<IActionResult> CreateReview(ReviewCreateRequestDto reviewDto)
        {
            await reviewService.Create(reviewDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("update-review")]
        public async Task<IActionResult> UpdateReview(ReviewUpdateRequestDto reviewDto)
        {
            await reviewService.Update(reviewDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Customer")]
        [HttpPut("update-status-review")]
        public async Task<IActionResult> UpdateStatusReview(ReviewStatusRequestDto reviewDto)
        {
            await reviewService.UpdateStatus(reviewDto);
            throw new MyException("Successfully.", 200);
        }
    }
}