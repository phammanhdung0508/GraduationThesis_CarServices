using AutoMapper;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly TokenConfiguration tokenConfiguration;
        private readonly EncryptConfiguration encryptConfiguration;

        public AuthenticationRepository(DataContext context, IMapper mapper,
        TokenConfiguration tokenConfiguration, EncryptConfiguration encryptConfiguration)
        {
            this.encryptConfiguration = encryptConfiguration;
            this.tokenConfiguration = tokenConfiguration;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserDto?> CheckLogin(LoginDto login)
        {
            try
            {
                UserDto? user = null;
                bool check = true;
                // string email = _encryptConfiguration.Base64Decode(login.user_email);
                string email = login.user_email;
                var _user = await context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.user_email == email);
                if (_user == null)
                {
                    check = false;
                    //throw 404
                }
                else
                {
                    if (!encryptConfiguration.VerifyPasswordHash(login.user_password, _user.password_hash, _user.password_salt))
                    {
                        check = false;
                        //throw 404
                    }
                }
                if (_user?.user_status == false)
                {
                    check = false;
                    //throw 404
                }
                if (check == true)
                {
                    user = mapper.Map<UserDto>(_user);
                    //check
                    Console.WriteLine(user.roleDto.role_name);

                    string token = tokenConfiguration.CreateToken(user);
                    user.user_token = token;
                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public RefreshTokenDto? RefreshToken()
        {
            try
            {
                var refreshToken = tokenConfiguration.GenerateRefreshToken();
                return refreshToken;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public async Task CreateUser(CreateUserDto _user)
        {
            try
            {
                encryptConfiguration.CreatePasswordHash(_user.user_password, out byte[] password_hash, out byte[] password_salt);
                User user = new User
                {
                    user_email = _user.user_email,
                    password_hash = password_hash,
                    password_salt = password_salt
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}