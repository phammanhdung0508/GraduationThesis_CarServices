using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Product;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IProductService
    {
        Task<List<ProductListResponseDto>?> View(PageDto page);
        Task<List<ProductListResponseDto>?> FilterServiceProduct(int ServiceId);
        Task<ProductDetailResponseDto?> Detail(int id);
        Task<bool> Create(ProductCreateRequestDto requestDto);
        Task<bool> Update(ProductUpdateRequestDto requestDto);
        Task<bool> UpdateStatus(ProductStatusRequestDto requestDto);
        Task<bool> UpdateQuantity(ProductQuantityRequestDto requestDto);

    }
}
