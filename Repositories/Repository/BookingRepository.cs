using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public BookingRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<BookingResponseDto>?> View(PageDto page)
        {
            try
            {
                List<Booking> list = await PagingConfiguration<Booking>
                .Get(context.Bookings.Include(b => b.Car)
                // .Include(b => b.Coupon).Include(b => b.Payment)
                .Include(b => b.Report).Include(b => b.Garage)
                .Include(b => b.Schedule), page);
                return mapper.Map<List<BookingResponseDto>>(list);
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
                BookingResponseDto booking = mapper.Map<BookingResponseDto>(await context.Bookings.Include(b => b.Car)
                // .Include(b => b.Coupon).Include(b => b.Payment)
                .Include(b => b.Report).Include(b => b.Garage)
                .Include(b => b.Schedule).FirstOrDefaultAsync(c => c.BookingId == id));
                return booking;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(Booking booking)
        {
            try
            {
                context.Bookings.Add(booking);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateBookingDto bookingDto)
        {
            try
            {
                var booking = context.Bookings.FirstOrDefault(c => c.BookingId == bookingDto.BookingId)!;
                mapper.Map<UpdateBookingDto, Booking?>(bookingDto, booking);
                context.Bookings.Update(booking);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteBookingDto bookingDto)
        {
            try
            {
                var booking = context.Bookings.FirstOrDefault(c => c.BookingId == bookingDto.BookingId)!;
                mapper.Map<DeleteBookingDto, Booking?>(bookingDto, booking);
                context.Bookings.Update(booking);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}