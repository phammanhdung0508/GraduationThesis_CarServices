
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService garageService;
        public GarageController(IGarageService garageService)
        {
            this.garageService = garageService;
        }

        [HttpPost("view-all-garage")]
        public async Task<ActionResult<List<GarageDto>>> ViewGarage(PageDto page)
        {
            try
            {
                var garageList = await garageService.View(page)!;
                return Ok(garageList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("detail-garage")]
        public async Task<ActionResult<GarageDto>> DetailGarage(int id)
        {
            try
            {
                var garage = await garageService.Detail(id);
                return Ok(garage);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-garage")]
        public async Task<ActionResult<bool>> CreateGarage(CreateGarageDto garage)
        {
            try
            {
                if (await garageService.Create(garage))
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

        [HttpPut("update-garage")]
        public async Task<ActionResult<bool>> UpdateGarage(UpdateGarageDto garage)
        {
            try
            {
                if (await garageService.Update(garage))
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

        [HttpPut("delete-garage")]
        public async Task<ActionResult<bool>> DeleteCoupon(DeleteGarageDto garage)
        {
            try
            {
                if (await garageService.Delete(garage))
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