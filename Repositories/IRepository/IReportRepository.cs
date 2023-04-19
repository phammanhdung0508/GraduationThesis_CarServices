using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IReportRepository
    {
        Task<List<ReportDto>?> View(PageDto page);
        Task<ReportDto?> Detail(int id);
        Task Create(CreateReportDto reportDto);
        Task Update(UpdateReportDto reportDto);
        Task Delete(DeleteReportDto reportDto);
    }
}