using AutoMapper;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly EncryptConfiguration encryptConfiguration;
        private readonly GeocoderConfiguration geocoderConfiguration;
        public UserService(IMapper mapper, IUserRepository userRepository,
        EncryptConfiguration encryptConfiguration, GeocoderConfiguration geocoderConfiguration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.encryptConfiguration = encryptConfiguration;
            this.geocoderConfiguration = geocoderConfiguration;
        }

        public async Task<List<UserListResponseDto>?> View(PageDto page)
        {

            try
            {
                var list = mapper
                .Map<List<UserListResponseDto>>(await userRepository.View(page));

                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDetailResponseDto?> Detail(int id)
        {
            try
            {
                var user = mapper.Map<UserDetailResponseDto>(await userRepository.Detail(id));
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(UserCreateRequestDto requestDto)
        {
            try
            {
                if (requestDto.UserPassword.Equals(requestDto.PasswordConfirm))
                {
                    encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);
                    var user = mapper.Map<UserCreateRequestDto, User>(requestDto,
                    opt => opt.AfterMap((src, des) =>
                    {
                        des.PasswordHash = password_hash;
                        des.PasswordSalt = password_salt;
                        des.UserGender = Gender.Male;
                        des.UserStatus = Status.Activate;
                        des.CreatedAt = DateTime.Now;
                        des.RoleId = 2;
                    }));
                    await userRepository.Create(user);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UserUpdateRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);
                var user = mapper.Map<UserUpdateRequestDto, User>(requestDto, u!,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await userRepository.Update(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRole(UserRoleRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);
                var user = mapper.Map<UserRoleRequestDto, User>(requestDto, u!);
                await userRepository.Update(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(UserStatusRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);
                var user = mapper.Map<UserStatusRequestDto, User>(requestDto, u!);
                await userRepository.Update(user);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}