using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using GraduationThesis_CarServices.Enum;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        public ProductRepository(DataContext context)
        {
            this.context = context;
        }


        public async Task<(List<Product>, int count)> View(PageDto page)
        {
            try
            {
                var query = context.Products.AsQueryable();

                var count = await query.CountAsync();
                
                var list = await PagingConfiguration<Product>.Get(query.Include(p => p.Category).Include(p => p.Service), page);

                return (list, count);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<(List<Product>, int count)> SearchByName(PageDto page, string searchString)
        {
            try
            {
                var searchTrim = searchString.Trim().Replace(" ", "").ToLower();
                var query = context.Products.Where(s => s.ProductName.ToLower().Trim().Replace(" ", "").Contains(searchTrim)).AsQueryable();
                
                var count = await query.CountAsync();

                var list = await PagingConfiguration<Product>.Get(query, page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> CountProductData()
        {
            try
            {
                var count = await context.Products.CountAsync();

                return count;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<bool> IsProductExist(int productId)
        {
            try
            {
                var check = await context.Products
                .Where(p => p.ProductId == productId).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>?> FilterAvailableProductForService(int serviceId)
        {
            try
            {
                var list = await context.Products
                .Where(p => p.ServiceId == serviceId && p.ProductQuantity > 0)
                .Include(p => p.Category)
                .Include(p => p.Service)
                .ToListAsync();

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product?> Detail(int id)
        {
            try
            {
                var product = await context.Products
                .Where(p => p.ProductId == id)
                .Include(p => p.Category)
                .Include(p => p.Service)
                .FirstOrDefaultAsync();
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsDuplicatedProduct(Product product)
        {
            try
            {
                var check = await context.Products
                .Where(p => p.ProductName.Equals(product.ProductName)
                && p.ProductPrice == product.ProductPrice
                && p.ProductStatus == Status.Activate).AnyAsync();

                return check;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Product product)
        {
            try
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(Product product)
        {
            try
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double GetPrice(int productId)
        {
            try
            {
                var price = context.Products
                .Where(p => p.ProductId.Equals(productId))
                .Select(p => p.ProductPrice).FirstOrDefault();

                return price;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
