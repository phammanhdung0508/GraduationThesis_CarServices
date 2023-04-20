using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class ReportService : IReportService
    {
        private readonly IMapper mapper;
        private readonly IReportRepository reportRepository;
        public ReportService(IMapper mapper, IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository;
            this.mapper = mapper;
        }

        public async Task<List<ReportDto>?> View(PageDto page)
        {

            try
            {
                List<ReportDto>? list = await reportRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ReportDto?> Detail(int id)
        {
            try
            {
                ReportDto? report = mapper.Map<ReportDto>(await reportRepository.Detail(id));
                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateReportDto createReportDto)
        {
            try
            {
                await reportRepository.Create(createReportDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateReportDto updateReportDto)
        {
            try
            {
                await reportRepository.Update(updateReportDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteReportDto deleteReportDto)
        {
            try
            {
                await reportRepository.Delete(deleteReportDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}