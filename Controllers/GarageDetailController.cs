using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;
using GraduationThesis_CarServices.Models.DTO.Exception;
using Microsoft.AspNetCore.Authorization;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/garage-detail")]
    [ApiController]
    public class GarageDetailController : ControllerBase
    {
        public readonly IGarageDetailService garageDetailService;

        public GarageDetailController(IGarageDetailService garageDetailService)
        {
            this.garageDetailService = garageDetailService;
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-garage-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            var garageDetail = await garageDetailService.Detail(id);
            return Ok(garageDetail);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-garage-detail")]
        public async Task<IActionResult> ViewGarageDetail(PageDto page)
        {
            var garageDetailList = await garageDetailService.View(page)!;
            return Ok(garageDetailList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-garage-detail")]
        public async Task<IActionResult> CreateGarageDetail(GarageDetailCreateRequestDto garageDetail)
        {
            await garageDetailService.Create(garageDetail);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-garage-detail")]
        public async Task<IActionResult> UpdateGarageDetail(GarageDetailUpdateRequestDto garageDetail)
        {
            await garageDetailService.Update(garageDetail);
            throw new MyException("Successfully.", 200);
        }

    }
}

