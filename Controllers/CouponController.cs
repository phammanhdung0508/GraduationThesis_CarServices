using GraduationThesis_CarServices.Models.DTO.Coupon;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Exception;
using Microsoft.AspNetCore.Authorization;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/coupon")]
    public class CouponController : ControllerBase
    {

        public readonly ICouponService couponService;

        public CouponController(ICouponService couponService)
        {
            this.couponService = couponService;
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("get-garage-coupon/{garageId}")]
        public async Task<IActionResult> GetGarageCoupon(int garageId)
        {
            var list = await couponService.FilterGarageCoupon(garageId)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-coupon/{id}")]
        public async Task<IActionResult> DetailCoupon(int id)
        {
            var coupon = await couponService.Detail(id);
            return Ok(coupon);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-coupon")]
        public async Task<IActionResult> View(PageDto page){
            var list = await couponService.View(page);
            return Ok(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-coupon")]
        public async Task<IActionResult> CreateCoupon(CouponCreateRequestDto couponCreateRequestDto)
        {
            await couponService.Create(couponCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-coupon")]
        public async Task<IActionResult> UpdateCoupon(CouponUpdateRequestDto couponUpdateRequestDto)
        {
            await couponService.Update(couponUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-coupon-status")]
        public async Task<IActionResult> UpdateStatus(CouponStatusRequestDto couponStatusRequestDto)
        {
            await couponService.UpdateStatus(couponStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}