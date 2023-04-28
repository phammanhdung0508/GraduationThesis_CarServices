using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public ProductRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<List<ProductDto>?> View(PageDto page)
        {
            try
            {
                List<Product> list = await PagingConfiguration<Product>.Get(context.Products, page);
                return mapper.Map<List<ProductDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDto?> Detail(int id)
        {
            try
            {
                ProductDto product = mapper.Map<ProductDto>(await context.Products.FirstOrDefaultAsync(c => c.ProductId == id));
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateProductDto productDto)
        {
            try
            {
                Product product = mapper.Map<Product>(productDto);
                context.Products.Add(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateProductDto productDto)
        {
            try
            {
                var product = context.Products.FirstOrDefault(c => c.ProductId == productDto.ProductId)!;
                mapper.Map<UpdateProductDto, Product?>(productDto, product);
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteProductDto productDto)
        {
            try
            {
                var product = context.Products.FirstOrDefault(c => c.ProductId == productDto.ProductId)!;
                mapper.Map<DeleteProductDto, Product?>(productDto, product);
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
