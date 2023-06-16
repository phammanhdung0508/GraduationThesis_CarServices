using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Report;
using GraduationThesis_CarServices.Models.Entity;
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
                var list = mapper.Map<List<ReportDto>>(await reportRepository.View(page));

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

        public async Task<ReportDto?> Detail(int id)
        {
            try
            {
                var report = mapper.Map<ReportDto>(await reportRepository.Detail(id));

                switch (false)
                {
                    case var isExist when isExist == (report != null):
                        throw new MyException("The report doesn't exist.", 404);
                }

                return report;
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

        public async Task Create(CreateReportDto requestDto)
        {
            try
            {
                var report = mapper.Map<CreateReportDto, Report>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.ReportStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));

                await reportRepository.Create(report);
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

        public async Task Update(UpdateReportDto requestDto)
        {
            try
            {
                var r = await reportRepository.Detail(requestDto.ReportId);

                switch (false)
                {
                    case var isExist when isExist == (r != null):
                        throw new MyException("The report doesn't exist.", 404);
                }

                var report = mapper.Map<UpdateReportDto, Report>(requestDto, r!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await reportRepository.Update(report);
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

        public async Task UpdateStatus(DeleteReportDto requestDto)
        {
            try
            {
                var r = await reportRepository.Detail(requestDto.ReportId);

                switch (false)
                {
                    case var isExist when isExist == (r != null):
                        throw new MyException("The report doesn't exist.", 404);
                }

                var report = mapper.Map<DeleteReportDto, Report>(requestDto, r!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await reportRepository.Update(report);
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