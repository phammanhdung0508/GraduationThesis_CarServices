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
                List<User> list = await PagingConfiguration<User>.Create(context.Users, page);
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
                UserDto user = mapper.Map<UserDto>(await context.Users.FirstOrDefaultAsync(g => g.user_id == id));
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
                var user = context.Users.FirstOrDefault(g => g.user_id == userDto.user_id)!;
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
                var user = context.Users.FirstOrDefault(g => g.user_id == userDto.user_id)!;
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