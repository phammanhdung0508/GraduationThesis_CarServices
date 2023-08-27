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

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("get-available-products-for-service/{serviceId}")]
        public async Task<IActionResult> GetAvailableProductsForService(int serviceId)
        {
            var productList = await productService.FilterAvailableProductForService(serviceId)!;
            return Ok(productList);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("search-products-by-name")]
        public async Task<IActionResult> SearchByName(SearchByNameRequestDto requestDto)
        {
            var productList = await productService.SearchByName(requestDto);
            return Ok(productList);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("detail-product/{id}")]
        public async Task<IActionResult> DetailProduct(int id)
        {
            var product = await productService.Detail(id);
            return Ok(product);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("view-all-product")]
        public async Task<IActionResult> ViewProduct(PageDto page)
        {
            var productList = await productService.View(page)!;
            return Ok(productList);

        }

        /// <summary>
        /// Creates new a product. [Admin]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Post create new product.
        ///     {
        ///         "ProductName": "Rửa xe Product",
        ///         "ProductImage": "Image",
        ///         "ProductDetailDescription": "Description",
        ///         "ProductPrice": 100.000 VND, /* 100.000 VND (string) */
        ///         "ProductQuantity": 10,
        ///         "ServiceId": 1,
        ///         "CategoryId": 1
        ///     }
        ///
        /// </remarks>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct(ProductCreateRequestDto product)
        {
            await productService.Create(product);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific product. [Admin]
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     Put update a specific product.
        ///     {
        ///         "ProductId": 1,
        ///         "ProductName": "Rửa xe Product New",
        ///         "ProductImage": "Image New",
        ///         "ProductDetailDescription": "Description New",
        ///         "ProductPrice": 100, /* 100 = 100k */
        ///         "ProductQuantity": 10,
        ///         "ServiceId": 1,
        ///         "CategoryId": 1
        ///     }
        ///
        /// </remarks>
        
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("update-product")]
        public async Task<IActionResult> Update(ProductUpdateRequestDto product)
        {
            await productService.Update(product);
            throw new MyException("Thành công.", 200);
        }

        /// <summary>
        /// Updates a specific product status. [Admin]
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("update-status/{productId}")]
        public async Task<IActionResult> UpdateStatus(int productId)
        {
            await productService.UpdateStatus(productId);
            throw new MyException("Thành công.", 200);

        }
    }
}

