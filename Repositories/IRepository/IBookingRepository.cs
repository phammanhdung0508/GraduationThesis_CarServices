using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IBookingRepository
    {
        Task<List<BookingDto>?> View(PageDto page);
        Task<BookingDto?> Detail(int id);
        Task Create(CreateBookingDto bookingDto);
        Task Update(UpdateBookingDto bookingDto);
        Task Delete(DeleteBookingDto bookingDto);
    }
}