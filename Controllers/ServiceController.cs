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

        /// <summary>
        /// View detail a specific Service.
        /// </summary>
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpGet("detail-service/{id}")]
        public async Task<IActionResult> DetailService(int id)
        {
            var service = await serviceService.Detail(id);
            return Ok(service);
        }

        /// <summary>
        /// Filter Service by specific garage, car type when booking. [Customer]
        /// </summary>
        [Authorize(Roles = "Customer")]
        [HttpGet("filter-service-by-garage/{garageId}&{carType}")]
        public async Task<IActionResult> GetServiceByServiceGroup(int garageId, int carType)
        {
            var serviceList = await serviceService.GetServiceByServiceGroup(garageId, carType);
            return Ok(serviceList);
        }

        /// <summary>
        /// View all services. [Customer]
        /// </summary>
        [HttpGet("get-all-service-list")]
        public async Task<IActionResult> GetAllService()
        {
            var list = await serviceService.GetAll();
            return Ok(list);
        }

        /// <summary>
        /// View all services. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("view-all-service")]
        public async Task<IActionResult> ViewService(PageDto page)
        {
            var serviceList = await serviceService.View(page)!;
            return Ok(serviceList);
        }

        /// <summary>
        /// Filter Service by specific garage. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager, Customer")]
        [HttpPost("filter-service-by-garage")]
        public async Task<IActionResult> FilterServiceByGarage(FilterByGarageRequestDto requestDto)
        {
            var serviceList = await serviceService.FilterServiceByGarage(requestDto)!;
            return Ok(serviceList);
        }

        /// <summary>
        /// Search service by name. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("search-services-by-name")]
        public async Task<IActionResult> SearchByName(SearchByNameRequestDto requestDto)
        {
            var serviceList = await serviceService.SearchByName(requestDto);
            return Ok(serviceList);
        }

        /// <summary>
        /// Creates new a service.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post create new service.
        ///     {
        ///         "serviceName": "Rửa xe",
        ///         "serviceImage": "Image",
        ///         "serviceGroup": 1,
        ///             1 : GÓI DỊCH VỤ VỆ SINH + BẢO DƯỠNG
        ///             2 : GÓI DỊCH VỤ NGOẠI THẤT
        ///             3 : GÓI DỊCH VỤ NỘI THẤT
        ///         "serviceWarrantyPeriod": 2
        ///         "serviceUnit": 1,
        ///             1 : Lần
        ///             2 : Gói
        ///         "serviceDetailDescription": "Description",
        ///         "serviceDuration": 1
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("create-service")]
        public async Task<IActionResult> CreateService(ServiceCreateRequestDto service)
        {
            await serviceService.Create(service);
            throw new MyException("Thành công.", 200);

        }

        /// <summary>
        /// Updates a specific service.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put update new service.
        ///     {
        ///         "ServiceId": 1,
        ///         "serviceName": "Rửa xe New",
        ///         "serviceImage": "Image New",
        ///         "serviceGroup": 2,
        ///             1 : GÓI DỊCH VỤ VỆ SINH + BẢO DƯỠNG
        ///             2 : GÓI DỊCH VỤ NGOẠI THẤT
        ///             3 : GÓI DỊCH VỤ NỘI THẤT
        ///         "serviceWarrantyPeriod": 2
        ///         "serviceUnit": 2,
        ///             1 : Lần
        ///             2 : Gói
        ///         "serviceDetailDescription": "Description New",
        ///         "serviceDuration": 2
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("update-service")]
        public async Task<IActionResult> UpdateService(ServiceUpdateRequestDto service)
        {
            await serviceService.Update(service);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific service status.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("update-status/{serviceId}")]
        public async Task<IActionResult> UpdateStatusService(int serviceId)
        {
            await serviceService.UpdateStatus(serviceId);
            throw new MyException("Thành công.", 200);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("get-not-avaliable-in-garage/{garageId}")]
        public async Task<IActionResult> GetNotSelectedServiceByGarage(int garageId)
        {
            var list = await serviceService.GetNotSelectedServiceByGarage(garageId);
            return Ok(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("get-all-id-and-name-garage")]
        public async Task<IActionResult> GetALLIdAndNameByGarage()
        {
            var list = await serviceService.GetALLIdAndNameByGarage();
            return Ok(list);
        }
    }
}