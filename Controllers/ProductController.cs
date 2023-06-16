using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduationThesis_CarServices.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("view-all-product")]
        public async Task<IActionResult> ViewProduct(PageDto page)
        {
            var productList = await productService.View(page)!;
            return Ok(productList);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-available-products-for-service/{serviceId}")]
        public async Task<IActionResult> GetAvailableProductsForService(int serviceId)
        {
            var productList = await productService.FilterAvailableProductForService(serviceId)!;
            return Ok(productList);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("detail-product/{id}")]
        public async Task<IActionResult> DetailProduct(int id)
        {
            var product = await productService.Detail(id);
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(ProductCreateRequestDto product)
        {
            await productService.Create(product);
            throw new MyException("Successfully.", 200);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-price-product")]
        public async Task<IActionResult> UpdatePriceProduct(ProductPriceRequestDto product)
        {
            await productService.UpdatePrice(product);
            throw new MyException("Successfully.", 200);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-status-product")]
        public async Task<IActionResult> UpdateStatusProduct(ProductStatusRequestDto product)
        {
            await productService.UpdateStatus(product);
            throw new MyException("Successfully.", 200);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-quantity-product")]
        public async Task<IActionResult> UpdateQuantityProduct(ProductQuantityRequestDto product)
        {
            await productService.UpdateQuantity(product);
            throw new MyException("Successfully.", 200);

        }
    }
}

