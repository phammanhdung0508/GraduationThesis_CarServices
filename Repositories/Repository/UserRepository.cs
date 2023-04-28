using AutoMapper;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper mapper;
        public readonly DataContext context;
        public UserRepository(IMapper mapper, DataContext context){
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>?> View(PageDto page)
        {
            try
            {
                var list = await PagingConfiguration<User>.Get(context.Users.Include(u => u.Role), page);
                return mapper.Map<List<UserDto>>(list);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDto?> Detail(int id)
        {
            try
            {
                UserDto user = mapper.Map<UserDto>(await context.Users.Include(u => u.Role).FirstOrDefaultAsync(g => g.UserId == id));
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Create(CreateUserDto userDto)
        {
            try
            {
                User user = mapper.Map<User>(userDto);
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Update(UpdateUserDto userDto)
        {
            try
            {
                var user = context.Users.FirstOrDefault(g => g.UserId == userDto.UserId)!;
                mapper.Map<UpdateUserDto, User?>(userDto, user);
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(DeleteUserDto userDto)
        {
            try
            {
                var user = context.Users.FirstOrDefault(g => g.UserId == userDto.UserId)!;
                mapper.Map<DeleteUserDto, User?>(userDto, user);
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