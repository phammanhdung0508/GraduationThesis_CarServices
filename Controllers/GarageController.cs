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

        [HttpPost("get-nearby-garages-location")]
        public async Task<ActionResult> GetNearbyGaragesLocation(LocationRequestDto locationRequestDto)
        {
            try
            {
                var list = await garageService.FilterGaragesNearMe(locationRequestDto)!;
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

        [HttpPost("get-garages-with-coupon")]
        public async Task<ActionResult> FilterGaragesWithCoupon(PageDto page)
        {
            try
            {
                var list = await garageService.FilterGaragesWithCoupon(page);
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

        [HttpPost("view-all-garage")]
        public async Task<ActionResult> ViewGarage(PageDto page)
        {
            try
            {
                var list = await garageService.View(page)!;
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

        [HttpGet("detail-garage/{id}")]
        public async Task<ActionResult> DetailGarage(int id)
        {
            try
            {
                var garage = await garageService.Detail(id);
                return Ok(garage);
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

        [HttpPost("create-garage")]
        public async Task<ActionResult> CreateGarage(GarageCreateRequestDto garageCreateRequestDto)
        {
            try
            {
                if (await garageService.Create(garageCreateRequestDto))
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

        [HttpPut("update-garage")]
        public async Task<ActionResult> UpdateGarage(GarageUpdateRequestDto garageUpdateRequestDto)
        {
            try
            {
                if (await garageService.Update(garageUpdateRequestDto))
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

        [HttpPut("update-status")]
        public async Task<ActionResult> UpdateStatus(GarageStatusRequestDto garageStatusRequestDto)
        {
            try
            {
                if (await garageService.UpdateStatus(garageStatusRequestDto))
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

        [HttpPut("update-location")]
        public async Task<ActionResult> UpdateLocation(LocationUpdateRequestDto locationUpdateRequestDto)
        {
            try
            {
                if (await garageService.UpdateLocation(locationUpdateRequestDto))
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