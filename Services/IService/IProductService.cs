using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<List<ProductListResponseDto>?> View(PageDto page);
        Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int serviceId);
        Task<ProductDetailResponseDto?> Detail(int id);
        Task Create(ProductCreateRequestDto requestDto);
        Task UpdatePrice(ProductPriceRequestDto requestDto);
        Task UpdateStatus(ProductStatusRequestDto requestDto);
        Task UpdateQuantity(ProductQuantityRequestDto requestDto);

    }
}
