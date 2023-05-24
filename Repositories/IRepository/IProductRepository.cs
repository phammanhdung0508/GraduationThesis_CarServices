using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<List<Product>?> View(PageDto page);
        Task<bool> IsProductExist(int productId);
        Task<List<Product>?> FilterAvailableProductForService(int ServiceId);
        Task<Product?> Detail(int id);
        Task<bool> IsDuplicatedProduct(Product product);
        Task Create(Product product);
        Task Update(Product product);
        float GetPrice(int productId);
    }
}
