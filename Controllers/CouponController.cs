using GraduationThesis_CarServices.Models.DTO.Coupon;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {

        public readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [HttpGet("get-garage-coupon/{garageId}")]
        public async Task<IActionResult> GetGarageCoupon(int garageId)
        {
            try
            {
                var list = await couponService.FilterGarageCoupon(garageId)!;
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

        [HttpGet("detail-coupon/{id}")]
        public async Task<IActionResult> DetailCoupon(int id)
        {
            try
            {
                var coupon = await couponService.Detail(id);
                return Ok(coupon);
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

        [HttpPost("create-coupon")]
        public async Task<IActionResult> CreateCoupon(CouponCreateRequestDto couponCreateRequestDto)
        {
            try
            {
                if (await couponService.Create(couponCreateRequestDto))
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

        [HttpPut("update-coupon")]
        public async Task<IActionResult> UpdateCoupon(CouponUpdateRequestDto couponUpdateRequestDto)
        {
            try
            {
                if (await couponService.Update(couponUpdateRequestDto))
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

        [HttpPut("update-coupon-status")]
        public async Task<IActionResult> UpdateStatus(CouponStatusRequestDto couponStatusRequestDto)
        {
            try
            {
                if (await couponService.UpdateStatus(couponStatusRequestDto))
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