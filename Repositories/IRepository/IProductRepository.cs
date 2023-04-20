using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDto>?> View(PageDto page);
        Task<ProductDto?> Detail(int id);
        Task Create(CreateProductDto ProductDto);
        Task Update(UpdateProductDto ProductDto);
        Task Delete(DeleteProductDto ProductDto);
    }
}
