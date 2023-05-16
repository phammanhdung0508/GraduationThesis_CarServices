using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface IServiceBookingRepository
    {
        Task Create(List<ServiceBooking> serviceBooking);
    }
}