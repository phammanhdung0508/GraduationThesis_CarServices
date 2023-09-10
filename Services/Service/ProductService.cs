using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
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

        public async Task<GenericObject<List<ProductListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count) = await productRepository.View(page);

                var listDto = mapper.Map<List<ProductListResponseDto>>(listObj);

                var list = new GenericObject<List<ProductListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<ProductListResponseDto>>> SearchByName(SearchByNameRequestDto requestDto)
        {
            try
            {
                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await productRepository.SearchByName(page, requestDto.Search);

                var listDto = mapper.Map<List<ProductListResponseDto>>(listObj);

                var list = new GenericObject<List<ProductListResponseDto>>(listDto, count);

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int serviceId)
        {
            try
            {
                var list = mapper
                .Map<List<ProductListResponseDto>>(await productRepository.FilterAvailableProductForService(serviceId));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
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
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Create(ProductCreateRequestDto requestDto)
        {
            try
            {
                var product = mapper.Map<ProductCreateRequestDto, Product>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ProductStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));

                if (!await productRepository.IsDuplicatedProduct(product))
                {
                    await productRepository.Create(product);
                }
                else
                {
                    throw new MyException("The product name is exist.", 404);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task Update(ProductUpdateRequestDto requestDto)
        {
            try
            {
                if (await productRepository.IsProductExist(requestDto.ProductId))
                {
                    switch (false)
                    {
                        case var isFail when isFail == requestDto.ProductPrice!.Contains("VND"):
                            throw new MyException("Vui lòng nhập đơn vị tiền tệ là VND", 404);
                    }
                    
                    var p = await productRepository.Detail(requestDto.ProductId);

                    var product = mapper.Map<ProductUpdateRequestDto, Product>(requestDto, p!);

                    await productRepository.Update(product);
                }
                else
                {
                    throw new MyException("The service doesn't exist.", 404);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }

        public async Task UpdateStatus(int productId)
        {
            try
            {
                if (await productRepository.IsProductExist(productId))
                {
                    var product = await productRepository.Detail(productId);

                    switch (product!.ProductStatus)
                    {
                        case Status.Activate:
                            product.ProductStatus = Status.Deactivate;
                            break;
                        case Status.Deactivate:
                            product.ProductStatus = Status.Activate;
                            break;
                    }

                    await productRepository.Update(product);
                }
                else
                {
                    throw new MyException("The service doesn't exist.", 404);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw;
                }
            }
        }
    }
}
