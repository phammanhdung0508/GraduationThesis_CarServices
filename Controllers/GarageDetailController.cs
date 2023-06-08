using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageDetailController : ControllerBase
    {
        public readonly IGarageDetailService garageDetailService;

        public GarageDetailController(IGarageDetailService garageDetailService)
        {
            this.garageDetailService = garageDetailService;

        }

        [HttpPost("view-all-garage-detail")]
        public async Task<IActionResult>ViewGarageDetail(PageDto page)
        {
            try
            {
                var garageDetailList = await garageDetailService.View(page)!;
                return Ok(garageDetailList);
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

        [HttpGet("detail-garage-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            try
            {
                var garageDetail = await garageDetailService.Detail(id);
                return Ok(garageDetail);
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

        [HttpPost("create-garage-detail")]
        public async Task<IActionResult> CreateGarageDetail(GarageDetailCreateRequestDto garageDetail)
        {
            try
            {
                if (await garageDetailService.Create(garageDetail))
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

        [HttpPut("update-garage-detail")]
        public async Task<IActionResult> UpdateGarageDetail(GarageDetailUpdateRequestDto garageDetail)
        {
            try
            {
                if (await garageDetailService.Update(garageDetail))
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

