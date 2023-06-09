using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.ServiceDetail;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDetailController : ControllerBase
    {
        public readonly IServiceDetailService serviceDetailService;

        public ServiceDetailController(IServiceDetailService serviceDetailService)
        {
            this.serviceDetailService = serviceDetailService;

        }

        [HttpPost("view-all-service-detail")]
        public async Task<IActionResult> ViewServiceDetail(PageDto page)
        {
            var serviceDetailList = await serviceDetailService.View(page)!;
            return Ok(serviceDetailList);
        }

        [HttpGet("filter-service-detail-by-service/{serviceId}")]
        public async Task<IActionResult> FilterServiceDetailByService(int serviceId)
        {
            var serviceDetailList = await serviceDetailService.FilterService(serviceId);
            return Ok(serviceDetailList);
        }

        [HttpGet("detail-service-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            var serviceDetail = await serviceDetailService.Detail(id);
            return Ok(serviceDetail);
        }

        [HttpPost("create-service-detail")]
        public async Task<IActionResult> CreateServiceDetail(ServiceDetailCreateRequestDto serviceDetail)
        {
            await serviceDetailService.Create(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-service-detail")]
        public async Task<IActionResult> UpdateServiceDetail(ServiceDetailUpdateRequestDto serviceDetail)
        {
            await serviceDetailService.Update(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-price-service-detail")]
        public async Task<IActionResult> UpdatePriceServiceDetail(ServiceDetailPriceRequestDto serviceDetail)
        {
            await serviceDetailService.UpdatePrice(serviceDetail);
            throw new MyException("Successfully.", 200);
        }

    }
}

