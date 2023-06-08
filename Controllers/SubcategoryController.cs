using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Subcategory;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubcategoryController : ControllerBase
    {
        public readonly ISubcategoryService subcategoryService;

        public SubcategoryController(ISubcategoryService subcategoryService)
        {
            this.subcategoryService = subcategoryService;
        }

        [HttpGet("detail-subcategory/{id}")]
        public async Task<IActionResult> DetailSubcategory(int id)
        {
            var schedule = await subcategoryService.Detail(id);
            return Ok(schedule);
        }

        [HttpPost("view-all-subcategory")]
        public async Task<IActionResult> ViewSubcategory(PageDto page)
        {
            var list = await subcategoryService.View(page)!;
            return Ok(list);
        }

        [HttpPost("create-subcategory")]
        public async Task<IActionResult> CreateSubcategory(CreateSubcategoryDto createSubcategoryDto)
        {
            await subcategoryService.Create(createSubcategoryDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-subcategory")]
        public async Task<IActionResult> UpdateSubcategory(UpdateSubcategoryDto updateSubcategoryDto)
        {
            await subcategoryService.Update(updateSubcategoryDto);
            throw new MyException("Successfully.", 200);
        }
    }
}