// using System.Diagnostics;
// using AutoMapper;
// using GraduationThesis_CarServices.Enum;
// using GraduationThesis_CarServices.Models.DTO.Exception;
// using GraduationThesis_CarServices.Models.DTO.Page;
// using GraduationThesis_CarServices.Models.DTO.Subcategory;
// using GraduationThesis_CarServices.Models.Entity;
// using GraduationThesis_CarServices.Repositories.IRepository;
// using GraduationThesis_CarServices.Services.IService;

// namespace GraduationThesis_CarServices.Services.Service
// {
//     public class SubcategoryService : ISubcategoryService
//     {
//         private readonly ISubcategoryRepository subcategoryRepository;
//         private readonly IMapper mapper;
//         public SubcategoryService(ISubcategoryRepository subcategoryRepository, IMapper mapper)
//         {
//             this.mapper = mapper;
//             this.subcategoryRepository = subcategoryRepository;
//         }

//         public async Task<List<SubcategoryDto>?> View(PageDto page)
//         {
//             try
//             {
//                 var list = mapper.Map<List<SubcategoryDto>>(await subcategoryRepository.View(page));

//                 return list;
//             }
//             catch (Exception e)
//             {
//                 switch (e)
//                 {
//                     case MyException:
//                         throw;
//                     default:
//                         var inner = e.InnerException;
//                         while (inner != null)
//                         {
//                             Console.WriteLine(inner.StackTrace);
//                             inner = inner.InnerException;
//                         }
//                         Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
//                         throw;
//                 }
//             }
//         }

//         public async Task<SubcategoryDto?> Detail(int id)
//         {
//             try
//             {
//                 var subcategory = mapper.Map<SubcategoryDto>(await subcategoryRepository.Detail(id));

//                 switch (false)
//                 {
//                     case var isExist when isExist == (subcategory != null):
//                         throw new MyException("The subcategory doesn't exist.", 404);
//                 }

//                 return subcategory;
//             }
//             catch (Exception e)
//             {
//                 switch (e)
//                 {
//                     case MyException:
//                         throw;
//                     default:
//                         var inner = e.InnerException;
//                         while (inner != null)
//                         {
//                             Console.WriteLine(inner.StackTrace);
//                             inner = inner.InnerException;
//                         }
//                         Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
//                         throw;
//                 }
//             }
//         }

//         public async Task Create(CreateSubcategoryDto requestDto)
//         {
//             try
//             {
//                 var subcategory = mapper.Map<CreateSubcategoryDto, Subcategory>(requestDto,
//                 otp => otp.AfterMap((src, des) =>
//                 {
//                     des.SubcategoryStatus = Status.Activate;
//                     des.CreatedAt = DateTime.Now;
//                 }));

//                 await subcategoryRepository.Create(subcategory);
//             }
//             catch (Exception e)
//             {
//                 switch (e)
//                 {
//                     case MyException:
//                         throw;
//                     default:
//                         var inner = e.InnerException;
//                         while (inner != null)
//                         {
//                             Console.WriteLine(inner.StackTrace);
//                             inner = inner.InnerException;
//                         }
//                         Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
//                         throw;
//                 }
//             }
//         }

//         public async Task Update(UpdateSubcategoryDto requestDto)
//         {
//             try
//             {
//                 var s = await subcategoryRepository.Detail(requestDto.SubcategoryId);

//                 switch (false)
//                 {
//                     case var isExist when isExist == (s != null):
//                         throw new MyException("The subcategory doesn't exist.", 404);
//                 }

//                 var subcategory = mapper.Map<UpdateSubcategoryDto, Subcategory>(requestDto, s!,
//                 otp => otp.AfterMap((src, des) => {
//                     des.UpdatedAt = DateTime.Now;
//                 }));

//                 await subcategoryRepository.Update(subcategory);
//             }
//             catch (Exception e)
//             {
//                 switch (e)
//                 {
//                     case MyException:
//                         throw;
//                     default:
//                         var inner = e.InnerException;
//                         while (inner != null)
//                         {
//                             Console.WriteLine(inner.StackTrace);
//                             inner = inner.InnerException;
//                         }
//                         Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
//                         throw;
//                 }
//             }
//         }
//     }
// }