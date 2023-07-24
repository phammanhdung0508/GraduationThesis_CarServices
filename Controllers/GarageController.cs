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

        /// <summary>
        /// View detail a specific Garage.
        /// </summary>
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-garage/{id}")]
        public async Task<IActionResult> DetailGarage(int id)
        {
            var garage = await garageService.Detail(id);
            return Ok(garage);
        }

        // [Authorize(Roles = "Customer")]
        // [HttpGet("get-all-garage-coordinates")]
        // public async Task<IActionResult> GetAllCoordinates()
        // {
        //     var list = await garageService.GetAllCoordinates();
        //     return Ok(list);
        // }

        /// <summary>
        /// Filter Garages by specific date, location and service. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("filter-garage-by-date-location-service")]
        public async Task<IActionResult> GetNearbyGaragesLocation(FilterGarageRequestDto requestDto)
        {
            var list = await garageService.FilterGaragesNearMe(requestDto)!;
            return Ok(list);
        }

        /// <summary>
        /// View all garages. [Customer]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-garage")]
        public async Task<IActionResult> ViewGarage(PageDto page)
        {
            var list = await garageService.View(page)!;
            return Ok(list);
        }

        /// <summary>
        /// View all garages. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-garage-for-admin")]
        public async Task<IActionResult> ViewAllForAdmin(PageDto page)
        {
            var list = await garageService.ViewAllForAdmin(page)!;
            return Ok(list);
        }

        /// <summary>
        /// Search garages. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("search-garage")]
        public async Task<IActionResult> SearchGarage(SearchDto search)
        {
            var list = await garageService.Search(search)!;
            return Ok(list);
        }

        /// <summary>
        /// Creates new a garage.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("create-garage")]
        public async Task<IActionResult> CreateGarage(GarageCreateRequestDto garageCreateRequestDto)
        {
            await garageService.Create(garageCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Updates a specific garage.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-garage")]
        public async Task<IActionResult> UpdateGarage(GarageUpdateRequestDto garageUpdateRequestDto)
        {
            await garageService.Update(garageUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Updates a specific garage status.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-garage-status")]
        public async Task<IActionResult> UpdateStatus(GarageStatusRequestDto garageStatusRequestDto)
        {
            await garageService.UpdateStatus(garageStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }

        /// <summary>
        /// Updates a specific garage location.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-location")]
        public async Task<IActionResult> UpdateLocation(LocationUpdateRequestDto locationUpdateRequestDto)
        {
            await garageService.UpdateLocation(locationUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}