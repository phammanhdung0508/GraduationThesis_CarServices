using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        /// <summary>
        /// View detail a specific Category.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("detail-category/{id}")]
        public async Task<IActionResult> DetailCategory(int id)
        {
            var category = await categoryService.Detail(id);
            return Ok(category);
        }

        /// <summary>
        /// Search categories by category name.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("search-categories-by-name")]
        public async Task<IActionResult> SearchByName(SearchByNameRequestDto requestDto)
        {
            var categoryList = await categoryService.SearchByName(requestDto);
            return Ok(categoryList);
        }

        /// <summary>
        /// View all Booking.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-category")]
        public async Task<IActionResult> ViewCategory(PageDto page)
        {
            var categoryList = await categoryService.View(page)!;
            return Ok(categoryList);
        }

        /// <summary>
        /// Creates new a category.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory(CategoryCreateRequestDto categoryCreateRequestDto)
        {
            await categoryService.Create(categoryCreateRequestDto);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific category.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequestDto categoryUpdateRequestDto)
        {
            await categoryService.Update(categoryUpdateRequestDto);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific Category status.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("update-category-status")]
        public async Task<IActionResult> UpdateStatus(CategoryStatusRequestDto categoryStatusRequestDto)
        {
            await categoryService.UpdateStatus(categoryStatusRequestDto);
            throw new MyException("Thành công.", 200);
        }
    }
}
