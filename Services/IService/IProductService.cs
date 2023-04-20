using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<List<ProductDto>?> View(PageDto page);
        Task<ProductDto?> Detail(int id);
        Task<bool> Create(CreateProductDto createProductDto);
        Task<bool> Update(UpdateProductDto updateProductDto);
        Task<bool> Delete(DeleteProductDto deleteProductDto);
    }
}
