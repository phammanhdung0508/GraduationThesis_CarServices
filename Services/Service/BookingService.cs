using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ICouponRepository couponRepository;
        private readonly ICarRepository carRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMapper mapper;
        public BookingService(IBookingRepository bookingRepository, 
        IMapper mapper, ICarRepository carRepository, ICouponRepository couponRepository,
        IGarageRepository garageRepository)
        {
            this.mapper = mapper;
            this.bookingRepository = bookingRepository;
            this.carRepository = carRepository;
            this.couponRepository = couponRepository;
            this.garageRepository = garageRepository;
        }

        public async Task<List<BookingDto>?> View(PageDto page)
        {
            try
            {
                List<BookingDto>? list = await bookingRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingDto?> Detail(int id)
        {
            try
            {
                BookingDto? car = await bookingRepository.Detail(id);
                return car;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateBookingDto createBookingDto)
        {
            try
            {
                await carRepository.Detail(createBookingDto.CarId);
                await couponRepository.Detail(createBookingDto.CouponId);
                await couponRepository.Detail(createBookingDto.GarageId);
                
                await bookingRepository.Create(createBookingDto);
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