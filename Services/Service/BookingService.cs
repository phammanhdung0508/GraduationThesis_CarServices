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
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository,
        IMapper mapper, ICarRepository carRepository, IScheduleRepository scheduleRepository,
        IServiceGarageRepository serviceGarageRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
            this.scheduleRepository = scheduleRepository;
        }

        public async Task<List<BookingListResponseDto>?> View(PageDto page)
        {
            try
            {
                var list = mapper
                .Map<List<BookingListResponseDto>>(await bookingRepository.View(page));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingDetailResponseDto?> Detail(int id)
        {
            try
            {
                var booking = mapper
                .Map<BookingDetailResponseDto>(await bookingRepository.Detail(id));

                return booking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(BookingCreateRequestDto requestDto)
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

                Booking booking = mapper.Map<BookingCreateRequestDto, Booking>(requestDto,
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

        public async Task<bool> Update(BookingUpdateRequestDto requestDto)
        {
            try
            {
                var b = await bookingRepository.Detail(requestDto.BookingId);
                var booking = mapper.Map<BookingUpdateRequestDto, Booking>(requestDto, b!,
                otp => otp.AfterMap((src, des) => {
                    des.UpdatedAt = DateTime.Now;
                }));
                await bookingRepository.Update(booking);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}