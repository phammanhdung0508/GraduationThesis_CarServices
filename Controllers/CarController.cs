using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        public readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpGet("get-user-car/{userId}")]
        public async Task<IActionResult> GetUserCar(int userId)
        {
            var car = await carService.FilterUserCar(userId);
            return Ok(car);
        }

        [HttpGet("detail-car/{id}")]
        public async Task<IActionResult> DetailCar(int id)
        {
            var car = await carService.Detail(id);
            return Ok(car);
        }

        [HttpPost("create-car")]
        public async Task<IActionResult> CreateCar(CarCreateRequestDto carCreateRequestDto)
        {
            await carService.Create(carCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-car")]
        public async Task<IActionResult> UpdateCar(CarUpdateRequestDto carUpdateRequestDto)
        {
            await carService.Update(carUpdateRequestDto);
            throw new MyException("Successfully.", 200);

        }

        [HttpPut("update-car-status")]
        public async Task<IActionResult> UpdateStatus(CarStatusRequestDto carStatusRequestDto)
        {

            await carService.UpdateStatus(carStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}