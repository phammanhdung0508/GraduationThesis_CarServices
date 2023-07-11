using System.IdentityModel.Tokens.Jwt;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Review;
using GraduationThesis_CarServices.Models.DTO.Service;
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

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-review/{id}")]
        public async Task<IActionResult> DetailReview(int id)
        {
            var review = await reviewService.Detail(id);
            return Ok(review);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-review")]
        public async Task<IActionResult> ViewAllReview(PageDto page)
        {
            var list = await reviewService.View(page)!;
            return Ok(list);
        }

        [HttpPost("search-review-by-customer-name")]
        public async Task<IActionResult> SearchByName(SearchByNameRequestDto requestDto)
        {
            var list = await reviewService.SearchByName(requestDto);
            return Ok(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("filter-all-review")]
        public async Task<IActionResult> FilterAllReview(ReviewFilterRequestDto requestDto)
        {
            var list = await reviewService.FilterAllReview(requestDto)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("filter-review-by-garage")]
        public async Task<IActionResult> FilterReviewByGarage(FilterByGarageRequestDto requestDto)
        {
            var list = await reviewService.FilterReviewByGarage(requestDto)!;
            return Ok(list);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("create-review")]
        public async Task<IActionResult> CreateReview(ReviewCreateRequestDto reviewDto)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            await reviewService.Create(reviewDto, userId);
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