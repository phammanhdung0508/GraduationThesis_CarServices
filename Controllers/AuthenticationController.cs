using CarServices.Models;
using CarServices.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarServices.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthenticationController(DataContext context){
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto request){
            try{
                return Ok();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout"), Authorize(Roles="")]
        public async Task<ActionResult> Logout(string request){
            try{
                return Ok();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("verify-access-token/{access-token}")]
        public async Task<IActionResult> VerifyAccessToken(string request){
            try{
                return Ok();
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }
    }
}