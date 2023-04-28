using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Page;

namespace GraduationThesis_CarServices.Services.IService
{
    public interface IBookingService
    {
        Task<List<BookingDto>?> View(PageDto page);
        Task<BookingDto?> Detail(int id);
        Task<bool> Create(CreateBookingDto createBookingDto);
        Task<bool> Update(UpdateBookingDto updateBookingDto);
        Task<bool> Delete(DeleteBookingDto deleteBookingDto);
    }
}