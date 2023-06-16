using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Service;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService serviceService;
        public ServiceController(IServiceService serviceService)
        {
            this.serviceService = serviceService;
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("view-all-service")]
        public async Task<IActionResult> ViewService(PageDto page)
        {
            var serviceList = await serviceService.View(page)!;
            return Ok(serviceList);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("get-available-services-for-garage/{garageId}")]
        public async Task<IActionResult> GetAvailableServicesForGarage(int garageId)
        {
            var serviceList = await serviceService.FilterServiceByGarage(garageId)!;
            return Ok(serviceList);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-service/{id}")]
        public async Task<IActionResult> DetailService(int id)
        {
            var service = await serviceService.Detail(id);
            return Ok(service);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-service")]
        public async Task<IActionResult> CreateService(ServiceCreateRequestDto service)
        {
            await serviceService.Create(service);
            throw new MyException("Successfully.", 200);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-service")]
        public async Task<IActionResult> UpdateService(ServiceUpdateRequestDto service)
        {
            await serviceService.Update(service);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-status-service")]
        public async Task<IActionResult> UpdateStatusService(ServiceStatusRequestDto service)
        {
            await serviceService.UpdateStatus(service);
            throw new MyException("Successfully.", 200);
        }

    }
}
