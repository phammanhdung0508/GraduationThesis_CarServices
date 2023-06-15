using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Models.DTO.Exception;
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
                var list = mapper.Map<List<UserListResponseDto>>(await userRepository.View(page));

                return list;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task<UserDetailResponseDto?> Detail(int id)
        {
            try
            {
                var user = mapper.Map<UserDetailResponseDto>(await userRepository.Detail(id));

                switch (false)
                {
                    case var isExist when isExist == (user != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                return user;
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UserRegister(UserCreateRequestDto requestDto)
        {
            try
            {
                switch (false)
                {
                    case var isExist when isExist == (requestDto.UserPassword.Equals(requestDto.PasswordConfirm)):
                        throw new MyException("Your password and confirm password isn't match.", 404);
                }

                encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);
                var encryptEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail);

                var user = mapper.Map<UserCreateRequestDto, User>(requestDto,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UserEmail = encryptEmail;
                    des.PasswordHash = password_hash;
                    des.PasswordSalt = password_salt;
                    des.UserStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                    des.RoleId = 2;
                }));

                await userRepository.Create(user);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task Update(UserUpdateRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);

                switch (false)
                {
                    case var isExist when isExist == (u != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                var user = mapper.Map<UserUpdateRequestDto, User>(requestDto, u!,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await userRepository.Update(user);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdateRole(UserRoleRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);

                switch (false)
                {
                    case var isExist when isExist == (u != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                var user = mapper.Map<UserRoleRequestDto, User>(requestDto, u!);
                await userRepository.Update(user);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }

        public async Task UpdateStatus(UserStatusRequestDto requestDto)
        {
            try
            {
                var u = await userRepository.Detail(requestDto.UserId);

                switch (false)
                {
                    case var isExist when isExist == (u != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                var user = mapper.Map<UserStatusRequestDto, User>(requestDto, u!);
                await userRepository.Update(user);
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case MyException:
                        throw;
                    default:
                        var inner = e.InnerException;
                        while (inner != null)
                        {
                            Console.WriteLine(inner.StackTrace);
                            inner = inner.InnerException;
                        }
                        Debug.WriteLine(e.Message + "\r\n" + e.StackTrace + "\r\n" + inner);
                        throw new MyException("Internal Server Error", 500);
                }
            }
        }
    }
}