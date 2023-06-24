using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;
using GraduationThesis_CarServices.Paging;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<GenericObject<List<ProductListResponseDto>>> View(PageDto page);
        Task<List<ProductListResponseDto>?> FilterAvailableProductForService(int serviceId);
        Task<ProductDetailResponseDto?> Detail(int id);
        Task Create(ProductCreateRequestDto requestDto);
        Task UpdatePrice(ProductPriceRequestDto requestDto);
        Task UpdateStatus(ProductStatusRequestDto requestDto);
        Task UpdateQuantity(ProductQuantityRequestDto requestDto);

    }
}
