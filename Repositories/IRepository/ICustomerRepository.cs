using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ICustomerRepository
    {
        Task<int> Create(Customer customer);
        Task<Customer?> Detail(int customerId);
    }
}