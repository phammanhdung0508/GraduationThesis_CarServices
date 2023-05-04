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
        private readonly ISubcategoryRepository subcategoryRepository;
        private readonly IServiceRepository serviceRepository;
        
        private readonly IMapper mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository, ISubcategoryRepository subcategoryRepository, IServiceRepository serviceRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
            this.subcategoryRepository = subcategoryRepository;
            this.serviceRepository = serviceRepository;
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
                await subcategoryRepository.Detail(createProductDto.SubcategoryId);
                await serviceRepository.Detail(createProductDto.ServiceId);
                
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
