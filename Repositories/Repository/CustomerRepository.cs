using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly DataContext context;
        public CustomerRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> Create(Customer customer)
        {
            try
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();

                return await context.Customers.OrderBy(c => c.CustomerId)
                .Select(c => c.CustomerId).LastAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Customer?> Detail(int customerId){
            try
            {
                var customer = await context.Customers.Include(c => c.User).Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();

                return customer;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}