using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<List<ProductListResponseDto>?> View(PageDto page);
        Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int ServiceId);
        Task<ProductDetailResponseDto?> Detail(int id);
        Task<bool> Create(ProductCreateRequestDto requestDto);
        Task<bool> UpdatePrice(ProductPriceRequestDto requestDto);
        Task<bool> UpdateStatus(ProductStatusRequestDto requestDto);
        Task<bool> UpdateQuantity(ProductQuantityRequestDto requestDto);

    }
}
