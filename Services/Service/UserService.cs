using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<List<UserDto>?> View(PageDto page)
        {

            try
            {
                List<UserDto>? list = await userRepository.View(page);
                return list;
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
                UserDto? user = mapper.Map<UserDto>(await userRepository.Detail(id));
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateUserDto createUserDto)
        {
            try
            {
                await userRepository.Create(createUserDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateUserDto updateUserDto)
        {
            try
            {
                await userRepository.Update(updateUserDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteUserDto deleteUserDto)
        {
            try
            {
                await userRepository.Delete(deleteUserDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}