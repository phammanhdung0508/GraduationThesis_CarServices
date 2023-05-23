using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        private readonly IMapper mapper;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<List<ProductListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<ProductListResponseDto>>(await productRepository.View(page));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int ServiceId)
        {
            try
            {
                var list = mapper
                .Map<List<ProductListResponseDto>>(await productRepository.FilterAvailableProductForService(ServiceId));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductDetailResponseDto?> Detail(int id)
        {
            try
            {
                var product = mapper
                .Map<ProductDetailResponseDto>(await productRepository.Detail(id));
                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(ProductCreateRequestDto requestDto)
        {
            try
            {
                var product = mapper.Map<ProductCreateRequestDto, Product>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ProductStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));
                await productRepository.Create(product);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(ProductUpdateRequestDto requestDto)
        {
            try
            {
                var p = await productRepository.Detail(requestDto.ProductId);
                var product = mapper.Map<ProductUpdateRequestDto, Product>(requestDto, p!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await productRepository.Update(product);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(ProductStatusRequestDto requestDto)
        {
            try
            {
                var p = await productRepository.Detail(requestDto.ProductId);
                var product = mapper.Map<ProductStatusRequestDto, Product>(requestDto, p!);
                await productRepository.Update(product);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> UpdateQuantity(ProductQuantityRequestDto requestDto)
        {
            try
            {
                var p = await productRepository.Detail(requestDto.ProductId);
                var product = mapper.Map<ProductQuantityRequestDto, Product>(requestDto, p!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await productRepository.Update(product);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
