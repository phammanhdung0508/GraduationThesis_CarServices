using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Category;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

        public async Task<GenericObject<List<CategoryListResponseDto>>?> View(PageDto page)
        {
            try
            {
                var list = mapper.Map<List<CategoryListResponseDto>>(await categoryRepository.View(page));

                var listCount = new GenericObject<List<CategoryListResponseDto>>(list, list.Count);

                return listCount;
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

        public async Task<CategoryDetailResponseDto?> Detail(int id)
        {
            try
            {
                var category = mapper.Map<CategoryDetailResponseDto>(await categoryRepository.Detail(id));

                switch (false)
                {
                    case var isExist when isExist == (category != null):
                        throw new MyException("The category doesn't exist.", 404);
                }

                return category;
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

        public async Task Create(CategoryCreateRequestDto requestDto)
        {
            try
            {
                var category = mapper.Map<CategoryCreateRequestDto, Category>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.CategoryStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                }));

                await categoryRepository.Create(category);
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

        public async Task Update(CategoryUpdateRequestDto requestDto)
        {
            try
            {
                var c = await categoryRepository.Detail(requestDto.CategoryId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The category doesn't exist.", 404);
                }

                var category = mapper.Map<CategoryUpdateRequestDto, Category>(requestDto, c!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));

                await categoryRepository.Update(category);
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

        public async Task UpdateStatus(CategoryStatusRequestDto requestDto)
        {
            try
            {
                var c = await categoryRepository.Detail(requestDto.CategoryId);

                switch (false)
                {
                    case var isExist when isExist == (c != null):
                        throw new MyException("The category doesn't exist.", 404);
                }

                var category = mapper.Map<CategoryStatusRequestDto, Category>(requestDto, c!);

                await categoryRepository.Update(category);
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
