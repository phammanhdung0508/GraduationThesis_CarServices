using GraduationThesis_CarServices.Models.DTO.Coupon;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        
        public ICouponService couponService { get; }
        
        public CouponController(ICouponService couponService){
            this.couponService = couponService;
            
        }

        [HttpPost("view-all-coupon")]
        public async Task<ActionResult<List<CouponDto>>> ViewCoupon(PageDto page)
        {
            try
            {
                var couponList = await couponService.View(page)!;
                return Ok(couponList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("detail-coupon")]
        public async Task<ActionResult<CouponDto>> DetailCoupon(int id){
            try{
                var coupon = await couponService.Detail(id);
                return Ok(coupon);
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-coupon")]
        public async Task<ActionResult<bool>> CreateCoupon(CreateCouponDto coupon)
        {
            try
            {
                if (await couponService.Create(coupon))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-coupon")]
        public async Task<ActionResult<bool>> UpdateCoupon(UpdateCouponDto coupon)
        {
            try
            {
                if (await couponService.Update(coupon))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("delete-coupon")]
        public async Task<ActionResult<bool>> DeleteCoupon(DeleteCouponDto coupon)
        {
            try
            {
                if (await couponService.Delete(coupon))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}