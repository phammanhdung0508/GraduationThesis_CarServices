using AutoMapper;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Models.DTO.GarageDetail;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Exception;
using System.Diagnostics;

namespace GraduationThesis_CarServices.Services.Service
{
    public class GarageDetailService : IGarageDetailService
    {
        private readonly IGarageDetailRepository garageDetailRepository;

        private readonly IMapper mapper;
        public GarageDetailService(IGarageDetailRepository garageDetailRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.garageDetailRepository = garageDetailRepository;
        }

        public async Task<List<GarageDetailDetailResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<GarageDetailDetailResponseDto>>(await garageDetailRepository.View(page));
                
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<GarageDetailDetailResponseDto?> Detail(int id)
        {
            try
            {
                var service = mapper.Map<GarageDetailDetailResponseDto>(await garageDetailRepository.Detail(id));
                return service;
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Create(GarageDetailCreateRequestDto requestDto)
        {
            try
            {
                var service = mapper.Map<GarageDetailCreateRequestDto, GarageDetail>(requestDto);
                await garageDetailRepository.Create(service);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Update(GarageDetailUpdateRequestDto requestDto)
        {
            try
            {
                var g = await garageDetailRepository.Detail(requestDto.GarageDetailId);
                var garageDetail = mapper.Map<GarageDetailUpdateRequestDto, GarageDetail>(requestDto, g!);
                await garageDetailRepository.Update(garageDetail);
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
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }
    }
}
