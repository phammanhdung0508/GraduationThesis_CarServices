using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;

        }

        [HttpPost("view-all-category")]
        public async Task<IActionResult> ViewCategory(PageDto page)
        {
            var categoryList = await categoryService.View(page)!;
            return Ok(categoryList);
        }

        [HttpGet("detail-category/{id}")]
        public async Task<IActionResult> DetailCategory(int id)
        {
            var category = await categoryService.Detail(id);
            return Ok(category);
        }

        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CategoryCreateRequestDto categoryCreateRequestDto)
        {
            await categoryService.Create(categoryCreateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequestDto categoryUpdateRequestDto)
        {
            await categoryService.Update(categoryUpdateRequestDto);
            throw new MyException("Successfully.", 200);
        }

        [HttpPut("update-category-status")]
        public async Task<IActionResult> UpdateStatus(CategoryStatusRequestDto categoryStatusRequestDto)
        {
            await categoryService.UpdateStatus(categoryStatusRequestDto);
            throw new MyException("Successfully.", 200);
        }
    }
}
