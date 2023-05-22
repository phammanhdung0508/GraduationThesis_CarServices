using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<List<ReviewListResponseDto>>> ViewReview(PageDto page)
        {
            try
            {
                var list = await reviewService.View(page)!;
                return Ok(list);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpGet("get-garage-reviews/{id}")]
        public async Task<ActionResult<List<ReviewListResponseDto>>> GetGarageReview(int id)
        {
            try
            {
                var list = await reviewService.FilterGarageReview(id)!;
                return Ok(list);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        // [HttpGet("detail-review/{id}")]
        // public async Task<ActionResult<ReviewDto>> DetailReview(int id)
        // {
        //     try
        //     {
        //         var review = await reviewService.Detail(id);
        //         return Ok(review);
        //     }
        //     catch (Exception e)
        //     {
        //         var inner = e.InnerException;
        //         while (inner != null)
        //         {
        //             Console.WriteLine(inner.StackTrace);
        //             inner = inner.InnerException;
        //         }
        //         return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
        //     }
        // }

        [HttpPost("create-review")]
        public async Task<ActionResult<bool>> CreateReview(ReviewCreateRequestDto reviewDto)
        {
            try
            {
                if (await reviewService.Create(reviewDto))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-review")]
        public async Task<ActionResult<bool>> UpdateReview(ReviewUpdateRequestDto reviewDto)
        {
            try
            {
                if (await reviewService.Update(reviewDto))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-status-review")]
        public async Task<ActionResult<bool>> UpdateStatusReview(ReviewStatusRequestDto reviewDto)
        {
            try
            {
                if (await reviewService.UpdateStatus(reviewDto))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }
    }
}