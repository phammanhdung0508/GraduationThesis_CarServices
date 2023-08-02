using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IProductRepository
    {
        Task<(List<Product>, int count)> View(PageDto page);
        Task<bool> IsProductExist(int productId);
        Task<List<Product>?> FilterAvailableProductForService(int serviceId);
        Task<Product?> Detail(int id);
        Task<bool> IsDuplicatedProduct(Product product);
        Task Create(Product product);
        Task Update(Product product);
        decimal GetPrice(int productId);
        Task<int> CountProductData();
        Task<(List<Product>, int count)> SearchByName(PageDto page, string searchString);
        Product GetDefaultProduct(int serviceDetailId);
    }
}
