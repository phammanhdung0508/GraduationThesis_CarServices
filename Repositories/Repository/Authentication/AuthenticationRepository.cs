using System.Diagnostics;
using AutoMapper;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models;
using GraduationThesis_CarServices.Models.DTO.Authentication;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Jwt;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using OtpNet;

namespace GraduationThesis_CarServices.Repositories.Repository.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        private readonly TokenConfiguration tokenConfiguration;
        private readonly EncryptConfiguration encryptConfiguration;
        private readonly IUserRepository userRepository;

        public AuthenticationRepository(DataContext context, IMapper mapper,
        TokenConfiguration tokenConfiguration, EncryptConfiguration encryptConfiguration, IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            this.encryptConfiguration = encryptConfiguration;
            this.tokenConfiguration = tokenConfiguration;
            this.context = context;
            this.mapper = mapper;
        }

        //Secret Key For One Time Password
        private readonly string secretKey = "BAAAJJJJ";

        public async Task<UserLoginDto?> CheckLogin(LoginDto login)
        {
            try
            {
                UserLoginDto? user = null;
                bool check = true;
                string email = encryptConfiguration.Base64Decode(login.Email);
                var _user = await context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.UserEmail == email);
                if (_user == null)
                {
                    check = false;
                    throw new MyException("Your email don't exist.", 404);
                }
                else
                {
                    if (!encryptConfiguration.VerifyPasswordHash(login.Password, _user.PasswordHash, _user.PasswordSalt))
                    {
                        check = false;
                        throw new MyException("Your password is not correct.", 404);
                    }
                }
                if (_user?.UserStatus == 0)
                {
                    check = false;
                    throw new MyException("Sorry your account have been disable.", 404);
                }
                if (check == true)
                {
                    user = mapper.Map<UserLoginDto>(_user);
                    //check
                    // Console.WriteLine(user.RoleDto.RoleName);

                    string token = tokenConfiguration.CreateToken(user);
                    user.UserToken = token;
                    return user;
                }
                return null;
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

        public async Task SendOTP(string recipientEmail)
        {
            try
            {
                byte[] secretBytes = Base32Encoding.ToBytes(secretKey);
                var totp = new Totp(secretBytes, step: 60);
                // Time step of 1 minute (60 seconds)

                DateTime currentTime = DateTime.UtcNow;
                currentTime = currentTime.AddSeconds(-currentTime.Second);
                // Round down to the nearest minute

                var otp = totp.ComputeTotp();

                await tokenConfiguration.Send(otp, recipientEmail);
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
                        throw;
                }
            }
        }

        public bool ValidateOTP(string otp)
        {
            byte[] secretBytes = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(secretBytes);
            return totp.VerifyTotp(otp, out _, VerificationWindow.RfcSpecifiedNetworkDelay);
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

        public async Task<JWTDto> AuthenFirebase(string idToken)
        {
            try
            {
                string key = "AIzaSyA-XIGm7ETxWnhFWQogHuPKgXBJ0LZ-euk";
                string jwt = "";
                JWTDto? jwtDto = null;
                FirebaseToken decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                string uid = decodedToken.Uid;
                var authenUser = new FirebaseAuthProvider(new FirebaseConfig(key));
                var authen = authenUser.GetUserAsync(idToken);
                User user = authen.Result;
                var tagetAccount = await context.Users.Include(a => a.Role)
                .Where(a => a.UserEmail == encryptConfiguration.Base64Encode(user.Email.ToLower()))
                .FirstOrDefaultAsync();
                if (tagetAccount!.UserStatus == Status.Deactivate)
                {
                    throw new BadHttpRequestException("Your account have been block!");
                }
                String email = encryptConfiguration.Base64Decode(tagetAccount.UserEmail);
                jwt = tokenConfiguration.ReCreateFirebaseToken(tagetAccount, uid);
                jwtDto = new JWTDto(tagetAccount.UserId, email, true, tagetAccount.UserFirstName + tagetAccount.UserLastName, user.PhotoUrl, jwt, tagetAccount.Role.RoleName);
                return (jwtDto);
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