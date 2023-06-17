using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContext context;
        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<User>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<User>
                .Get(context.Users.Include(u => u.Role), page);

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsCustomerExist(int customerId)
        {
            try
            {
                var isExist = await context.Customers
                .Where(l => l.CustomerId == customerId).AnyAsync();

                return isExist;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetCustomerId(int userId){
            try
            {
                var customerId = await context.Users.Include(u => u.Customer).Where(u => u.UserId == userId).Select(u => u.Customer.CustomerId).FirstOrDefaultAsync();

                return customerId;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<User?> Detail(int id)
        {
            try
            {
                var user = await context.Users.Include(u => u.Role).Include(u => u.Customer)
                .FirstOrDefaultAsync(g => g.UserId == id);

                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> Create(User user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();

                return context.Users
                .OrderByDescending(b => b.UserId)
                .Select(b => b.UserId).First();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(User user)
        {
            try
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> FilterByRole(PageDto page, int roleId)
        {
            try
            {
                var list = await PagingConfiguration<User>.Get(context.Users.Include(u => u.Customer).Include(u => u.Role).Where(u => u.RoleId == roleId), page);

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public int TotalBooking(int customerId)
        {
            try
            {
                var totalBooking = 0;
                var listCars = context.Cars.Where(c => c.CustomerId == customerId).Include(c => c.Bookings).ToList();
                for (int i = 0; i < listCars.Count; i++)
                {
                    var bookingCount = listCars[i].Bookings.Count;
                    totalBooking += bookingCount;
                }

                return totalBooking;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}