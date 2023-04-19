
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReportService
    {
        Task<List<ReportDto>?> View(PageDto page);
        Task<ReportDto?> Detail(int id);
        Task<bool> Create(CreateReportDto createReportDto);
        Task<bool> Update(UpdateReportDto updateReportDto);
        Task<bool> Delete(DeleteReportDto deleteReportDto);
    }
}