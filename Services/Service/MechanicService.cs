using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Mechanic;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class MechanicService : IMechanicService
    {
        private readonly IMapper mapper;
        private readonly IMechanicRepository mechanicRepository;
        private readonly IGarageRepository garageRepository;

        public MechanicService(IMapper mapper, IMechanicRepository mechanicRepository, IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.mechanicRepository = mechanicRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<GenericObject<List<MechanicListResponseDto>>> View(PageDto page)
        {
            try
            {
                (var listObj, var count, var listTotal) = await mechanicRepository.View(page);

                var listDto = mapper.Map<List<MechanicListResponseDto>>(listObj,
                obj => obj.AfterMap((src, des) =>
                {
                    for (int i = 0; i < des.Count; i++)
                    {
                        des[i].TotalOrders = listTotal[i];
                    }
                }));

                var list = new GenericObject<List<MechanicListResponseDto>>(listDto, count);

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

        public async Task<GenericObject<List<MechanicListResponseDto>>> FilterMechanicsByGarage(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count, var listTotal) = await mechanicRepository.FilterMechanicAvailableByGarageId(requestDto.GarageId, page);

                var listDto = mapper.Map<List<MechanicListResponseDto>>(listObj,
                obj => obj.AfterMap((src, des) =>
                {
                    for (int i = 0; i < des.Count; i++)
                    {
                        des[i].TotalOrders = listTotal[i];
                    }
                }));

                var list = new GenericObject<List<MechanicListResponseDto>>(listDto, count);

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

        public async Task<List<MechanicListResponseDto>> FilterMechanicsByBooking(int bookingId)
        {
            try
            {
                var list = mapper.Map<List<MechanicListResponseDto>>
                (await mechanicRepository.GetMechanicByBooking(bookingId));

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

        public async Task<MechanicDetailResponseDto?> Detail(int mechanicId)
        {
            try
            {
                var mechanic = await mechanicRepository.Detail(mechanicId);

                var countBookingMechanicApplied = await mechanicRepository.CountBookingMechanicApplied(mechanic!.MechanicId);

                var bookingCurrent = await mechanicRepository.GetBookingMechanicCurrentWorkingOn(mechanic!.MechanicId);

                var bookingDto = bookingCurrent is null ? null : mapper.Map<BookingMechanicCurrentWorkingOn>(bookingCurrent);

                return false switch
                {
                    var isExist when isExist == (mechanic is not null) => throw new MyException("The mechanic doesn't exist.", 404),
                    _ => mapper.Map<MechanicDetailResponseDto>(mechanic,
                                    obj => obj.AfterMap((src, des) =>
                                    {
                                        des.TotalBookingApplied = countBookingMechanicApplied;
                                        des.BookingMechanicCurrentWorkingOn = bookingDto;
                                    })),
                };
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

        // public async Task<List<WorkingScheduleListResponseDto>> FilterWorkingSchedulesByMechanicId(int mechanicId)
        // {
        //     try
        //     {
        //         var isMechanicExist = await mechanicRepository.IsMechanicExist(mechanicId);

        //         switch (false)
        //         {
        //             case var isExist when isExist == isMechanicExist:
        //                 throw new MyException("The mechanic doesn't exist.", 404);
        //         }

        //         var list = mapper.Map<List<WorkingScheduleListResponseDto>>(await mechanicRepository.FilterWorkingSchedulesByMechanicId(mechanicId));

        //         return list;
        //     }
        //     catch (Exception e)
        //     {
        //         switch (e)
        //         {
        //             case MyException:
        //                 throw;
        //             default:
        //                 var inner = e.InnerException;
        //                 while (inner != null)
        //                 {
        //                     Console.WriteLine(inner.StackTrace);
        //                     inner = inner.InnerException;
        //                 }
        //                 Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
        //                 throw;
        //         }
        //     }
        // }

        public async Task<List<MechanicWorkForGarageResponseDto>> GetMechanicByGarage(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var list = mapper.Map<List<MechanicWorkForGarageResponseDto>>
                (await mechanicRepository.FilterMechanicsAvailableByGarage(garageId, true));

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

        public async Task<List<MechanicWorkForBookingResponseDto>> GetMechanicByBooking(int bookingId)
        {
            try
            {
                var list = mapper.Map<List<MechanicWorkForBookingResponseDto>>
                (await mechanicRepository.GetMechanicByBooking(bookingId));

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

        public async Task AddAvaliableMechanicToBooking(EditMechanicBookingRequestDto requestDto)
        {
            try
            {
                var isExist = await mechanicRepository.IsMechanicExist(requestDto.MechanicId);

                switch (false)
                {
                    case var isFalse when isFalse ==
                    (requestDto.MechanicId != 0 &&
                    requestDto.BookingId != 0):
                        throw new MyException("MechanicId and BookingId dont take 0 value.", 404);
                    case var isFalse when isFalse == isExist:
                        throw new MyException("The mechanic doesn't exist.", 404);
                }

                var bookingMechanic = mapper.Map<BookingMechanic>(requestDto);

                await mechanicRepository.CreateBookingMechanic(bookingMechanic);
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

        public async Task RemoveMechanicfromBooking(EditMechanicBookingRequestDto requestDto)
        {
            try
            {

                switch (false)
                {
                    case var isFalse when isFalse ==
                    (requestDto.MechanicId != 0 &&
                    requestDto.BookingId != 0):
                        throw new MyException("MechanicId and BookingId dont take 0 value.", 404);
                }

                var bookingMechanic = await mechanicRepository.DetailBookingMechanic(requestDto.MechanicId, requestDto.BookingId);
                if (bookingMechanic is not null)
                {
                    bookingMechanic!.BookingMechanicStatus = Status.Deactivate;
                    await mechanicRepository.UpdateBookingMechanic(bookingMechanic);
                }
                else
                {
                    throw new MyException("Thợ không tồn tại", 404);
                }

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

        public async Task<GenericObject<List<BookingListResponseDto>>> GetBookingMechanicApplied(FilterBookingByMechanicRequestDto requestDto)
        {
            try
            {
                var page = new PageDto
                {
                    PageIndex = requestDto.PageIndex,
                    PageSize = requestDto.PageSize
                };

                (var listObj, var count) = await mechanicRepository.GetBookingMechanicApplied(requestDto.UserId, page);

                var listDto = mapper.Map<List<BookingListResponseDto>>(listObj);

                var list = new GenericObject<List<BookingListResponseDto>>(listDto, count);

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

        public async Task<List<MechanicWorkForGarageResponseDto>> GetMechanicAvaliableByGarage(int garageId)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(garageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var list = mapper.Map<List<MechanicWorkForGarageResponseDto>>
                (await mechanicRepository.GetMechanicAvaliableByGarage(garageId));

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
    }
}