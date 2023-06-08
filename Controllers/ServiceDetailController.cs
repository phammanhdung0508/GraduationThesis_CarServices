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
        public async Task<IActionResult>ViewServiceDetail(PageDto page)
        {
            try
            {
                var serviceDetailList = await serviceDetailService.View(page)!;
                return Ok(serviceDetailList);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpGet("filter-service-detail-by-service/{serviceId}")]
        public async Task<IActionResult> FilterServiceDetailByService(int serviceId)
        {
            try
            {
                var serviceDetailList = await serviceDetailService.FilterService(serviceId);
                return Ok(serviceDetailList);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpGet("detail-service-detail/{id}")]
        public async Task<IActionResult> DetailGarageDetail(int id)
        {
            try
            {
                var serviceDetail = await serviceDetailService.Detail(id);
                return Ok(serviceDetail);
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPost("create-service-detail")]
        public async Task<IActionResult> CreateServiceDetail(ServiceDetailCreateRequestDto serviceDetail)
        {
            try
            {
                if (await serviceDetailService.Create(serviceDetail))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-service-detail")]
        public async Task<IActionResult> UpdateServiceDetail(ServiceDetailUpdateRequestDto serviceDetail)
        {
            try
            {
                if (await serviceDetailService.Update(serviceDetail))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

        [HttpPut("update-price-service-detail")]
        public async Task<IActionResult> UpdatePriceServiceDetail(ServiceDetailPriceRequestDto serviceDetail)
        {
            try
            {
                if (await serviceDetailService.UpdatePrice(serviceDetail))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                var inner = e.InnerException;
                while (inner != null)
                {
                    Console.WriteLine(inner.StackTrace);
                    inner = inner.InnerException;
                }
                return BadRequest(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
            }
        }

    }
}

