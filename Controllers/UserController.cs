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

        /// <summary>
        /// View detail a specific User.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("detail-user/{id}")]
        public async Task<IActionResult> DetailUser(int id)
        {
            var user = await userService.Detail(id);
            return Ok(user);
        }

        /// <summary>
        /// View detail a specific Customer.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("detail-customer/{userId}")]
        public async Task<IActionResult> CustomerDetail(int userId)
        {
            var customer = await userService.CustomerDetail(userId);
            return Ok(customer);
        }

        /// <summary>
        /// Search Customer by role.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("search-by-role/{search}&{roleId}")]
        public async Task<IActionResult> SearchUser(string search, int roleId)
        {
            switch (roleId)
            {
                case 1:
                    var listCustomer = await userService.SearchCustomer(search);
                    return Ok(listCustomer);
                default:
                    var listUser = await userService.SearchUser(search, roleId);
                    return Ok(listUser);
            }
        }

        /// <summary>
        /// View all user. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-user")]
        public async Task<IActionResult> ViewUser(PageDto page)
        {
            var list = await userService.View(page)!;
            return Ok(list);
        }

        /// <summary>
        /// Filter Customer by role. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("filter-by-role/{roleId}")]
        public async Task<IActionResult> FilterByRole(PageDto page, int roleId)
        {
            switch (roleId)
            {
                case 1:
                    var listCustomer = await userService.FilterCustomer(page);
                    return Ok(listCustomer);
                default:
                    var listUser = await userService.FilterUser(page, roleId);
                    return Ok(listUser);
            }
        }

        /// <summary>
        /// Creates new customer, manager, staff. [Admin, Manager]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserCreateRequestDto requestDto)
        {
            int? garageId = null;
            if (requestDto.RoleId == 2 ||
            requestDto.RoleId == 5)
            {
                string encodedToken = HttpContext.Items["Token"]!.ToString()!;

                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(encodedToken);

                garageId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "garageId")!.Value);
            }

            await userService.Create(requestDto, garageId);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Creates new a mechanic. [Manager]
        /// </summary>
        [Authorize(Roles = "Manager")]
        [HttpPost("create-mechanic")]
        public async Task<IActionResult> CreateMechanic(MechanicCreateRequestDto requestDto)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            var garageId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "garageId")!.Value);

            await userService.CreateMechanic(requestDto, garageId);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific user. [Customer, Staff]
        /// </summary>
        [Authorize(Roles = "Customer, Staff")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUserFirstLogin(UserUpdateRequestDto userUpdateRequestDto)
        {
            string encodedToken = HttpContext.Items["Token"]!.ToString()!;

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);

            int userId = Int32.Parse(token.Claims.FirstOrDefault(c => c.Type == "userId")!.Value);

            var refeshToken = await userService.CustomerFirstLoginUpdate(userUpdateRequestDto, userId);

            if (refeshToken is not null)
            {
                return Ok(refeshToken);
            }

            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific user status. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateStatus(UserStatusRequestDto userStatusRequestDto)
        {
            await userService.UpdateStatus(userStatusRequestDto);
            throw new MyException("Thành công.", 200);
        }
    }
}