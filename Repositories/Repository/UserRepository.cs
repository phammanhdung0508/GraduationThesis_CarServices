using System.Text.RegularExpressions;
using GraduationThesis_CarServices.Enum;
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

        public async Task<bool> IsUserPhoneExist(string userPhone)
        {
            try
            {
                var isExist = await context.Users.Where(u => u.UserPhone.Equals(userPhone)).AnyAsync();

                return isExist;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsEmailExist(string userEmail)
        {
            try
            {
                var list = await context.Users.Select(u => u.UserEmail).ToListAsync();

                var isExist = list.Any(l => l.Equals(userEmail));

                return isExist;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<User?> GetUserByEmail(string userEmail)
        {
            try
            {
                var user = await context.Users.Where(u => u.UserEmail.Equals(userEmail)).FirstOrDefaultAsync();

                return user;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<bool> IsVerifyOtp(string inputString)
        {
            try
            {
                string pattern = @"\+84?\d{10}";
                var match = Regex.Match(inputString, pattern);

                var isVerify = context.Users.AsQueryable();

                switch (match.Success)
                {
                    case true:
                        return await isVerify.Where(u => u.UserPhone.Equals(inputString) && u.EmailConfirmed == 1).AnyAsync();
                    case false:
                        return await isVerify.Where(u => u.UserEmail.Equals(inputString) && u.EmailConfirmed == 1).AnyAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<User>?> SearchUser(string search, int roleId)
        {
            try
            {
                string pattern = @"\+84?\d{10}";

                if (Regex.Match(search, pattern).Success is false &&
                search.All(char.IsDigit) is true)
                {
                    search = "+84" + search!.Substring(1, 9);
                }

                var match = Regex.Match(search, pattern);

                var list = context.Users.Include(c => c.Customer).Include(c => c.Role).AsQueryable();
                var searchTrim = search.Trim().Replace(" ", "").ToLower();

                switch (match.Success)
                {
                    case true:
                        return await list.Where(c => c.UserPhone.Equals(search) && c.RoleId == roleId).ToListAsync();
                    case false:
                        return await list.Where(c => (c.UserFirstName.ToLower().Trim() + c.UserLastName.ToLower().Trim()).Contains(searchTrim)
                        || c.UserFirstName.ToLower().Contains(searchTrim)
                        || c.UserLastName.ToLower().Contains(searchTrim)).Where(c => c.RoleId == roleId).ToListAsync();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<int> GetCustomerId(int userId)
        {
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

        public async Task<User?> CustomerDetail(int userId)
        {
            try
            {
                var customer = await context.Users
                .Include(c => c.Customer).ThenInclude(c => c.Cars)
                .Where(c => c.UserId == userId).FirstOrDefaultAsync();

                return customer;
            }
            catch (System.Exception)
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

                return await context.Users
                .OrderByDescending(b => b.UserId)
                .Select(b => b.UserId).FirstAsync();
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

        public async Task<(List<User>?, int)> FilterByRole(PageDto page, int roleId, int garageId)
        {
            try
            {
                switch (roleId)
                {
                    case 35:
                        if (garageId != 0)
                        {
                            var mechanicList = await context.Users.Include(u => u.Role)
                            .Include(u => u.Mechanic).ThenInclude(u => u.GarageMechanics)
                            .Where(u => u.RoleId != 1 &&
                            u.RoleId != 2 &&
                            u.RoleId != 4 &&
                            u.RoleId != null &&
                            u.UserStatus.Equals(Status.Activate) &&
                            u.Mechanic.GarageMechanics.Any(g => g.GarageId == garageId)).ToListAsync();

                            var managerId = await context.Users
                            .Include(u => u.Garages).Where(u => u.RoleId == 2 &&
                            u.Garages.Any(g => g.GarageId == garageId))
                            .Select(u => u.UserId).FirstOrDefaultAsync();

                            var staffList = await context.Users
                            .Include(u => u.Role)
                            .Where(u => u.RoleId == 5 &&
                            u.ManagerId == managerId).ToListAsync();

                            var newList = mechanicList.Concat(staffList);

                            var count_ = newList.Count();

                            var list_ = await PagingConfiguration<User>.Get(newList.AsQueryable(), page);

                            return (list_, count_);
                        }
                        else
                        {
                            return (null, 0);
                        }
                    case 24:
                        var __query = context.Users
                        .Include(u => u.Role).Where(u => u.RoleId != 1 &&
                        u.RoleId != 3 &&
                        u.RoleId != 5).AsQueryable();

                        var __count = await __query.CountAsync();

                        var __list = await PagingConfiguration<User>.Get(__query, page);

                        return (__list, __count);
                    case 245:
                        var query = context.Users
                        .Include(u => u.Role).Where(u => u.RoleId != 1 && u.RoleId != 3).AsQueryable();

                        var count = await query.CountAsync();

                        var list = await PagingConfiguration<User>.Get(query, page);

                        return (list, count);
                    default:
                        IQueryable<User> _query;

                        if (roleId == 5 &&
                        garageId != 0)
                        {
                            var managerId = await context.Users
                            .Include(u => u.Garages).Where(u => u.RoleId == 2 &&
                            u.Garages.Any(g => g.GarageId == garageId))
                            .Select(u => u.UserId).FirstOrDefaultAsync();

                            _query = context.Users.Include(u => u.Customer)
                            .Include(u => u.Garages)
                            .Include(u => u.Role).Where(u => u.RoleId == roleId &&
                            u.ManagerId == managerId).AsQueryable();
                        }
                        else
                        {
                            _query = context.Users.Include(u => u.Customer)
                            .Include(u => u.Role).Where(u => u.RoleId == roleId).AsQueryable();
                        }

                        var _count = await _query.CountAsync();

                        var _list = await PagingConfiguration<User>.Get(_query, page);

                        return (_list, _count);
                }
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

        public async Task<(List<User>, int)> GetStaffByGarage(PageDto page, int garageId)
        {
            try
            {
                var query = context.Users.Include(u => u.Garages).Include(u => u.Role)
                .Where(u => u.Garages.Any(g => g.GarageId == garageId))
                .AsQueryable();

                var count = await query.CountAsync();

                var list = await PagingConfiguration<User>.Get(query, page);

                return (list, count);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> GetManagerNotAssignByGarage()
        {
            try
            {
                var avaliablelist = await context.Users
                .Where(u => u.RoleId == 2)
                .Join(context.Garages, u => u.UserId, g => g.UserId, (u, g) => u).ToListAsync();

                var Fulllist = await context.Users
                .Where(u => u.RoleId == 2).ToListAsync();

                var list = Fulllist.Except(avaliablelist).ToList();

                return list;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}