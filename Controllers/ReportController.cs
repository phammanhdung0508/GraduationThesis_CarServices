// using GraduationThesis_CarServices.Models.DTO.Exception;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.Report;
// using GraduationThesis_CarServices.Services.IService;
// using Microsoft.AspNetCore.Mvc;

// namespace GraduationThesis_CarServices.Controllers
// {
//     [ApiController]
//     [Route("api/report")]
//     public class ReportController : ControllerBase
//     {
//         private readonly IReportService reportService;
//         public ReportController(IReportService reportService)
//         {
//             this.reportService = reportService;
//         }

//         [HttpPost("view-all-report")]
//         public async Task<IActionResult> ViewCoupon(PageDto page)
//         {
//             var list = await reportService.View(page)!;
//             return Ok(list);
//         }

//         [HttpGet("detail-report/{id}")]
//         public async Task<IActionResult> DetailCoupon(int id)
//         {
//             var report = await reportService.Detail(id);
//             return Ok(report);
//         }

//         [HttpPost("create-report")]
//         public async Task<IActionResult> CreateCoupon(CreateReportDto reportDto)
//         {
//             await reportService.Create(reportDto);
//             throw new MyException("Successfully.", 200);
//         }

//         [HttpPut("update-report")]
//         public async Task<IActionResult> UpdateCoupon(UpdateReportDto reportDto)
//         {
//             await reportService.Update(reportDto);
//             throw new MyException("Successfully.", 200);
//         }

//         [HttpPut("delete-report")]
//         public async Task<IActionResult> DeleteCoupon(DeleteReportDto reportDto)
//         {
//             await reportService.UpdateStatus(reportDto);
//             throw new MyException("Successfully.", 200);
//         }
//     }
// }