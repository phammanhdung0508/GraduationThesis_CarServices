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

        [HttpPost("view-all-subcategory")]
        public async Task<ActionResult<List<SubcategoryDto>>> ViewCategory(PageDto page)
        {
            try
            {
                var list = await subcategoryService.View(page)!;
                return Ok(list);
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

        [HttpGet("detail-subcategory/{id}")]
        public async Task<ActionResult<SubcategoryDto>> DetailCategory(int id)
        {
            try
            {
                var schedule = await subcategoryService.Detail(id);
                return Ok(schedule);
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

        [HttpPost("create-subcategory")]
        public async Task<ActionResult<bool>> CreateCategory(CreateSubcategoryDto SubcategoryDto)
        {
            try
            {
                if (await subcategoryService.Create(SubcategoryDto))
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

        [HttpPut("update-subcategory")]
        public async Task<ActionResult<bool>> UpdateCategory(UpdateSubcategoryDto SubcategoryDto)
        {
            try
            {
                if (await subcategoryService.Update(SubcategoryDto))
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

        [HttpPut("delete-subcategory")]
        public async Task<ActionResult<bool>> DeleteCategory(DeleteSubcategoryDto SubcategoryDto)
        {
            try
            {
                if (await subcategoryService.Delete(SubcategoryDto))
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