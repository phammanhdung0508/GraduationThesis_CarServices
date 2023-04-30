using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Schedule;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly IScheduleRepository scheduleRepository;
        private readonly IServiceGarageRepository serviceGarageRepository;
        private readonly ICarRepository carRepository;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository,
        IMapper mapper, ICarRepository carRepository, IScheduleRepository scheduleRepository,
        IServiceGarageRepository serviceGarageRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
            this.scheduleRepository = scheduleRepository;
            this.serviceGarageRepository = serviceGarageRepository;
        }

        public async Task<List<BookingResponseDto>?> View(PageDto page)
        {
            try
            {
                List<BookingResponseDto>? list = await bookingRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingResponseDto?> Detail(int id)
        {
            try
            {
                BookingResponseDto? car = await bookingRepository.Detail(id);
                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CheckBooking(CreateRequestBookingDto requestDto)
        {
            try
            {
                ServiceGarage? serviceGarage = await serviceGarageRepository.Detail(requestDto.GarageId);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateRequestBookingDto requestDto)
        {
            try
            {
                DateTime bookingTime = DateOnly.Parse(requestDto.DateSelected).ToDateTime(TimeOnly.Parse(requestDto.TimeSelected));
                ScheduleDto scheduleDto = new ScheduleDto
                {
                    WorkDescription = "",
                    ScheduleStatus = 0
                };
                Schedule schedule = mapper.Map<ScheduleDto, Schedule>(scheduleDto,
                opt => opt.AfterMap((src, des) => des.BookingTime = bookingTime));
                await scheduleRepository.Create(schedule);

                Booking booking = mapper.Map<CreateRequestBookingDto, Booking>(requestDto,
                opt => opt.AfterMap((src, des) =>
                {
                    DateTime now = DateTime.Now;
                    des.BookingTime = bookingTime;
                    des.CreatedAt = now;
                    if (bookingTime.Date < now.Date)
                    {
                        des.BookingStatus = BookingStatus.NotStart;
                    }
                    else
                    {
                        des.BookingStatus = BookingStatus.AppointmentDay;
                    }
                    des.ScheduleId = schedule.ScheduleId;
                }));
                await bookingRepository.Create(booking);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateBookingDto updateBookingDto)
        {
            try
            {
                await bookingRepository.Update(updateBookingDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteBookingDto deleteBookingDto)
        {
            try
            {
                await bookingRepository.Delete(deleteBookingDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}