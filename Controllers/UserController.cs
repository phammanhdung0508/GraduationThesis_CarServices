using System.IdentityModel.Tokens.Jwt;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-user")]
        public async Task<IActionResult> ViewUser(PageDto page)
        {
            var list = await userService.View(page)!;
            return Ok(list);
        }

        [AllowAnonymous]
        [HttpGet("detail-user/{id}")]
        public async Task<IActionResult> DetailUser(int id)
        {
            var user = await userService.Detail(id);
            return Ok(user);
        }

        [HttpGet("detail-customer/{userId}")]
        public async Task<IActionResult> CustomerDetail(int userId)
        {
            var customer = await userService.CustomerDetail(userId);
            return Ok(customer);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("filter-by-role/{roleId}")]
        public async Task<IActionResult> FilterByRole(PageDto page, int roleId)
        {
            var list = await userService.FilterByRole(page, roleId);
            return Ok(list);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto userCreateRequestDto)
        {
            await userService.Create(userCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequestDto userUpdateRequestDto)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            await userService.CustomerFirstLoginUpdate(userUpdateRequestDto, userId);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(UserRoleRequestDto userRoleRequestDto)
        {
            await userService.UpdateRole(userRoleRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(UserStatusRequestDto userStatusRequestDto)
        {
            await userService.UpdateStatus(userStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}