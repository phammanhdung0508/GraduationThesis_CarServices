using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO.Exception;

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
        public async Task<IActionResult> ViewGarageDetail(PageDto page)
        {
            var garageDetailList = await garageDetailService.View(page)!;
            return Ok(garageDetailList);
        }

        [HttpGet("detail-garage-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            var garageDetail = await garageDetailService.Detail(id);
            return Ok(garageDetail);
        }

        [HttpPost("create-garage-detail")]
        public async Task<IActionResult> CreateGarageDetail(GarageDetailCreateRequestDto garageDetail)
        {
            await garageDetailService.Create(garageDetail);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-garage-detail")]
        public async Task<IActionResult> UpdateGarageDetail(GarageDetailUpdateRequestDto garageDetail)
        {
            await garageDetailService.Update(garageDetail);
            throw new MyException("Successfully.", 200);
        }

    }
}

