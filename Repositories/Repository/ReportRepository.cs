using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class ReportRepository : IReportRepository
    {
        public IMapper mapper { get; }
        public DataContext context { get; }
        public ReportRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ReportDto>?> View(PageDto page)
        {
            try
            {
                List<Report> list = await PagingConfiguration<Report>.Create(context.Reports, page);
                return mapper.Map<List<ReportDto>>(list);
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
                ReportDto report = mapper.Map<ReportDto>(await context.Coupons.FirstOrDefaultAsync(c => c.coupon_id == id));
                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateReportDto reportDto)
        {
            try
            {
                Report report = mapper.Map<Report>(reportDto);
                context.Reports.Add(report);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateReportDto reportDto)
        {
            try
            {
                var report = context.Reports.FirstOrDefault(r => r.report_id == reportDto.report_id)!;
                mapper.Map<UpdateReportDto, Report?>(reportDto, report);
                context.Reports.Update(report);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteReportDto reportDto)
        {
            try
            {
                var report = context.Reports.FirstOrDefault(r => r.report_id == reportDto.report_id)!;
                mapper.Map<DeleteReportDto, Report?>(reportDto, report);
                context.Reports.Update(report);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}