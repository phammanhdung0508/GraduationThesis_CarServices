
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IReportService
    {
        Task<List<ReportDto>?> View(PageDto page);
        Task<ReportDto?> Detail(int id);
        Task Create(CreateReportDto requestDto);
        Task Update(UpdateReportDto requestDto);
        Task UpdateStatus(DeleteReportDto requestDto);
    }
}