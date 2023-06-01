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

        public async Task<User?> Detail(int id)
        {
            try
            {
                var user = await context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(g => g.UserId == id);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(User user)
        {
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
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
    }
}