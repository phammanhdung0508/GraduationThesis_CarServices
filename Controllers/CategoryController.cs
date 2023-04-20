using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public ICategoryService categoryService { get; }

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;

        }

        [HttpPost("view-all-category")]
        public async Task<ActionResult<List<CategoryDto>>> ViewCategory(PageDto page)
        {
            try
            {
                var categoryList = await categoryService.View(page)!;
                return Ok(categoryList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("detail-category")]
        public async Task<ActionResult<CategoryDto>> DetailCategory(int id)
        {
            try
            {
                var category = await categoryService.Detail(id);
                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-category")]
        public async Task<ActionResult<bool>> CreateCategory(CreateCategoryDto category)
        {
            try
            {
                if (await categoryService.Create(category))
                {
                    return Ok("Successfully!");
                };
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<ActionResult<bool>> UpdateCategory(UpdateCategoryDto category)
        {
            try
            {
                if (await categoryService.Update(category))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("delete-category")]
        public async Task<ActionResult<bool>> DeleteCategory(DeleteCategoryDto category)
        {
            try
            {
                if (await categoryService.Delete(category))
                {
                    return Ok("Successfully!");
                }
                return BadRequest("Fail!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
