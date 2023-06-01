using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService reviewService;
        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost("view-all-review")]
        public async Task<IActionResult> ViewAllReview(PageDto page)
        {
            var list = await reviewService.View(page)!;
            return Ok(list);
        }

        [HttpPost("get-garage-reviews")]
        public async Task<IActionResult> GerReviewPerGarage(PagingReviewPerGarageRequestDto requestDto)
        {
            var list = await reviewService.FilterReviewByGarageId(requestDto)!;
            return Ok(list);
        }

        [HttpGet("detail-review/{id}")]
        public async Task<IActionResult> DetailReview(int id)
        {
            var review = await reviewService.Detail(id);
            return Ok(review);
        }

        [HttpPost("create-review")]
        public async Task<IActionResult> CreateReview(ReviewCreateRequestDto reviewDto)
        {
            await reviewService.Create(reviewDto);
            throw new Exception("Successfully.");
        }

        [HttpPut("update-review")]
        public async Task<IActionResult> UpdateReview(ReviewUpdateRequestDto reviewDto)
        {
            await reviewService.Update(reviewDto);
            throw new Exception("Successfully.");
        }

        [HttpPut("update-status-review")]
        public async Task<IActionResult> UpdateStatusReview(ReviewStatusRequestDto reviewDto)
        {
            await reviewService.UpdateStatus(reviewDto);
            throw new Exception("Successfully.");
        }
    }
}