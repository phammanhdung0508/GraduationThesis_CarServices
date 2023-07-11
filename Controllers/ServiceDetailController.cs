using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/service-detail")]
    [ApiController]
    public class ServiceDetailController : ControllerBase
    {
        public readonly IServiceDetailService serviceDetailService;

        public ServiceDetailController(IServiceDetailService serviceDetailService)
        {
            this.serviceDetailService = serviceDetailService;

        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("filter-service-detail-by-service/{serviceId}")]
        public async Task<IActionResult> FilterServiceDetailByService(int serviceId)
        {
            var serviceDetailList = await serviceDetailService.FilterService(serviceId);
            return Ok(serviceDetailList);
        }

        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-service-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            var serviceDetail = await serviceDetailService.Detail(id);
            return Ok(serviceDetail);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("view-all-service-detail")]
        public async Task<IActionResult> ViewServiceDetail(PageDto page)
        {
            var serviceDetailList = await serviceDetailService.View(page)!;
            return Ok(serviceDetailList);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-service-detail")]
        public async Task<IActionResult> CreateServiceDetail(ServiceDetailCreateRequestDto serviceDetail)
        {
            await serviceDetailService.Create(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-service-detail")]
        public async Task<IActionResult> UpdateServiceDetail(ServiceDetailUpdateRequestDto serviceDetail)
        {
            await serviceDetailService.Update(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-price-service-detail")]
        public async Task<IActionResult> UpdatePriceServiceDetail(ServiceDetailPriceRequestDto serviceDetail)
        {
            await serviceDetailService.UpdatePrice(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

    }
}

