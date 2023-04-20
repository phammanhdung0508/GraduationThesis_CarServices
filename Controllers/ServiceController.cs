using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService serviceService;
        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [HttpPost("view-all-service")]
        public async Task<ActionResult<List<ServiceDto>>> ViewService(PageDto page)
        {
            try
            {
                var serviceList = await serviceService.View(page)!;
                return Ok(serviceList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("detail-service")]
        public async Task<ActionResult<ServiceDto>> DetailService(int id)
        {
            try
            {
                var service = await serviceService.Detail(id);
                return Ok(service);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-service")]
        public async Task<ActionResult<bool>> CreateService(CreateServiceDto service)
        {
            try
            {
                if (await serviceService.Create(service))
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

        [HttpPut("update-service")]
        public async Task<ActionResult<bool>> UpdateService(UpdateServiceDto service)
        {
            try
            {
                if (await serviceService.Update(service))
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

        //Temporary don't make delete function because there's no service status

    }
}
