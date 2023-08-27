using System.IdentityModel.Tokens.Jwt;
using GraduationThesis_CarServices.Models.DTO.Car;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        public readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        /// <summary>
        /// View Cars a specific user owns.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("get-user-car")]
        public async Task<IActionResult> GetUserCar()
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            var car = await carService.FilterUserCar(userId);
            return Ok(car);
        }

        /// <summary>
        /// View detail of a specific Car.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("detail-car/{id}")]
        public async Task<IActionResult> DetailCar(int id)
        {
            var car = await carService.Detail(id);
            return Ok(car);
        }

        /// <summary>
        /// Creates new a Car.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPost("create-car")]
        public async Task<IActionResult> CreateCar(CarCreateRequestDto carCreateRequestDto)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            await carService.Create(carCreateRequestDto, userId);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific Car.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPut("update-car")]
        public async Task<IActionResult> UpdateCar(CarUpdateRequestDto carUpdateRequestDto)
        {
            await carService.Update(carUpdateRequestDto);
            throw new MyException("Thành công.", 200);

        }

        /// <summary>
        /// Updates a specific Car status only.
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpPut("update-car-status")]
        public async Task<IActionResult> UpdateStatus(CarStatusRequestDto carStatusRequestDto)
        {

            await carService.UpdateStatus(carStatusRequestDto);
            throw new MyException("Thành công.", 200);
        }
    }
}