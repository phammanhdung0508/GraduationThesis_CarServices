using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/garage")]
    public class GarageController : ControllerBase
    {
        private readonly IGarageService garageService;
        public GarageController(IGarageService garageService)
        {
            this.garageService = garageService;
        }

        [AllowAnonymous]
        [HttpPost("get-nearby-garages-location")]
        public async Task<IActionResult> GetNearbyGaragesLocation(LocationRequestDto locationRequestDto)
        {
            var list = await garageService.FilterGaragesNearMe(locationRequestDto)!;
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("get-garages-with-coupon")]
        public async Task<IActionResult> FilterGaragesWithCoupon(PageDto page)
        {
            var list = await garageService.FilterGaragesWithCoupon(page);
            return Ok(list);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("view-all-garage")]
        public async Task<IActionResult> ViewGarage(PageDto page)
        {
            var list = await garageService.View(page)!;
            return Ok(list);
        }

        //[Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("search-garage")]
        public async Task<IActionResult> SearchGarage(SearchDto search)
        {
            var list = await garageService.Search(search)!;
            return Ok(list);
        }

        //[Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-garage/{id}")]
        public async Task<IActionResult> DetailGarage(int id)
        {
            var garage = await garageService.Detail(id);
            return Ok(garage);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-garage")]
        public async Task<IActionResult> CreateGarage(GarageCreateRequestDto garageCreateRequestDto)
        {
            await garageService.Create(garageCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-garage")]
        public async Task<IActionResult> UpdateGarage(GarageUpdateRequestDto garageUpdateRequestDto)
        {
            await garageService.Update(garageUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-garage-status")]
        public async Task<IActionResult> UpdateStatus(GarageStatusRequestDto garageStatusRequestDto)
        {
            await garageService.UpdateStatus(garageStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-location")]
        public async Task<IActionResult> UpdateLocation(LocationUpdateRequestDto locationUpdateRequestDto)
        {
            await garageService.UpdateLocation(locationUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}