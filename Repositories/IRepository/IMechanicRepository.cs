using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IMechanicRepository
    {
        Task<(List<Mechanic>, int, List<int>)> View(PageDto page);
        Task<List<Mechanic>> FilterMechanicsAvailableByGarage(int garageId, bool isLv3);
        Task<List<Mechanic>> GetMechanicByBooking(int bookingId);
        Task<Mechanic?> Detail(int mechanicId);
        Task<bool> IsMechanicExist(int mechanicId);
        // Task<List<WorkingSchedule>> FilterWorkingSchedulesByMechanicId(int mechanicId);
        Task<List<Mechanic>> FilterMechanicAvailableByGarageId(int garageId);
        Task<int> Create(Mechanic mechanic);
        Task Update(Mechanic mechanic);
        Task<BookingMechanic?> DetailBookingMechanic(int mechanicId, int bookingId);
        Task CreateBookingMechanic(BookingMechanic bookingMechanic);
        Task UpdateBookingMechanic(BookingMechanic bookingMechanic);
        Task<BookingMechanic?> IsCustomerPickMainMechanic(DateTime date);
        Task<int> CountBookingMechanicApplied(int mechanicId);

        Task<(List<Booking>?, int count)> GetBookingMechanicApplied(int userId, PageDto page);
        Task<List<Mechanic>> GetMechanicAvaliableByGarage(int garageId);
    }
}