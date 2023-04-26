using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<List<ProductDto>?> View(PageDto page)
        {
            try
            {
                List<ProductDto>? list = await productRepository.View(page);
                return list;
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
                ProductDto? _product = mapper.Map<ProductDto>(await productRepository.Detail(id));
                return _product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateProductDto createProductDto)
        {
            try
            {
                await productRepository.Create(createProductDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateProductDto updateProductDto)
        {
            try
            {
                await productRepository.Update(updateProductDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteProductDto deleteProductDto)
        {
            try
            {
                await productRepository.Delete(deleteProductDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
