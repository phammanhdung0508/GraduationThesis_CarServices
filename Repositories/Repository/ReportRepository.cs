// using GraduationThesis_CarServices.Repositories.IRepository;
// using GraduationThesis_CarServices.Models;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.Entity;
// using GraduationThesis_CarServices.Paging;
// using Microsoft.EntityFrameworkCore;

// namespace GraduationThesis_CarServices.Repositories.Repository
// {
//     public class ReportRepository : IReportRepository
//     {
//         public DataContext context { get; }
//         public ReportRepository(DataContext context)
//         {
//             this.context = context;
//         }

//         public async Task<List<Report>?> View(PageDto page)
//         {
//             try
//             {
//                 var list = await PagingConfiguration<Report>.Get(context.Reports, page);

//                 return list;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task<Report?> Detail(int id)
//         {
//             try
//             {
//                 var report = await context.Reports.FirstOrDefaultAsync(c => c.ReportId == id);

//                 return report;
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task Create(Report report)
//         {
//             try
//             {
//                 context.Reports.Add(report);
//                 await context.SaveChangesAsync();
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }

//         public async Task Update(Report report)
//         {
//             try
//             {
//                 context.Reports.Update(report);
//                 await context.SaveChangesAsync();
//             }
//             catch (Exception)
//             {
//                 throw;
//             }
//         }
//     }
// }