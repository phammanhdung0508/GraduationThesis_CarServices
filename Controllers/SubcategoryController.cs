// using GraduationThesis_CarServices.Models.DTO.Exception;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.Subcategory;
// using GraduationThesis_CarServices.Services.IService;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace GraduationThesis_CarServices.Controllers
// {
//     [ApiController]
//     [Route("api/subcategory")]
//     public class SubcategoryController : ControllerBase
//     {
//         public readonly ISubcategoryService subcategoryService;

//         public SubcategoryController(ISubcategoryService subcategoryService)
//         {
//             this.subcategoryService = subcategoryService;
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpGet("detail-subcategory/{id}")]
//         public async Task<IActionResult> DetailSubcategory(int id)
//         {
//             var schedule = await subcategoryService.Detail(id);
//             return Ok(schedule);
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpPost("view-all-subcategory")]
//         public async Task<IActionResult> ViewSubcategory(PageDto page)
//         {
//             var list = await subcategoryService.View(page)!;
//             return Ok(list);
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpPost("create-subcategory")]
//         public async Task<IActionResult> CreateSubcategory(CreateSubcategoryDto createSubcategoryDto)
//         {
//             await subcategoryService.Create(createSubcategoryDto);
//             throw new MyException("Successfully.", 200);
//         }

//         [Authorize(Roles = "Admin")]
//         [HttpPut("update-subcategory")]
//         public async Task<IActionResult> UpdateSubcategory(UpdateSubcategoryDto updateSubcategoryDto)
//         {
//             await subcategoryService.Update(updateSubcategoryDto);
//             throw new MyException("Successfully.", 200);
//         }
//     }
// }