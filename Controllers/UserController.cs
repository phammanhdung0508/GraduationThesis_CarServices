using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("view-all-user")]
        public async Task<IActionResult> ViewUser(PageDto page)
        {
            var list = await userService.View(page)!;
            return Ok(list);
        }

        [HttpGet("detail-user/{id}")]
        public async Task<IActionResult> DetailUser(int id)
        {
            var user = await userService.Detail(id);
            return Ok(user);
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto userCreateRequestDto)
        {
            await userService.UserRegister(userCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequestDto userUpdateRequestDto)
        {
            await userService.Update(userUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-role")]
        public async Task<IActionResult> UpdateRole(UserRoleRequestDto userRoleRequestDto)
        {
            await userService.UpdateRole(userRoleRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(UserStatusRequestDto userStatusRequestDto)
        {
            await userService.UpdateStatus(userStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}