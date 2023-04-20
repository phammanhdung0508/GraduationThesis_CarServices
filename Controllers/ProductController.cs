using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IProductService productService { get; }

        public ProductController(IProductService productService)
        {
            this.productService = productService;

        }

        [HttpPost("view-all-product")]
        public async Task<ActionResult<List<ProductDto>>> ViewProduct(PageDto page)
        {
            try
            {
                var productList = await productService.View(page)!;
                return Ok(productList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("detail-product")]
        public async Task<ActionResult<ProductDto>> DetailProduct(int id)
        {
            try
            {
                var product = await productService.Detail(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<bool>> CreateProduct(CreateProductDto product)
        {
            try
            {
                if (await productService.Create(product))
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

        [HttpPut("update-product")]
        public async Task<ActionResult<bool>> UpdateProduct(UpdateProductDto product)
        {
            try
            {
                if (await productService.Update(product))
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

        [HttpPut("delete-product")]
        public async Task<ActionResult<bool>> DeleteProduct(DeleteProductDto product)
        {
            try
            {
                if (await productService.Delete(product))
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

