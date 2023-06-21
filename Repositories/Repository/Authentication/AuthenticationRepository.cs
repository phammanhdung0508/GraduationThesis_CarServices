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
using GraduationThesis_CarServices.Paging;
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
        private readonly ICustomerRepository customerRepository;

        public AuthenticationRepository(DataContext context, IMapper mapper,
        TokenConfiguration tokenConfiguration, EncryptConfiguration encryptConfiguration, IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
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
                string email = encryptConfiguration.Base64Encode(login.Email);

                var isEmailVerify = await userRepository.IsEmailVerifyOtp(email);

                if (!isEmailVerify)
                {
                    throw new MyException("Tài khoản này chưa được xác thực.", 404);
                }

                var _user = await context.Users.Include(r => r.Role).FirstOrDefaultAsync(u => u.UserEmail.Equals(email));
                if (_user == null)
                {
                    check = false;
                    throw new MyException("Tài khoản đăng nhập không tồn tại.", 404);
                }
                else
                {
                    if (!encryptConfiguration.VerifyPasswordHash(login.Password, _user.PasswordHash, _user.PasswordSalt))
                    {
                        check = false;
                        throw new MyException("Mật khẩu xác nhận không khớp.", 404);
                    }
                }
                if (_user?.UserStatus == 0)
                {
                    check = false;
                    throw new MyException("Xin lỗi, tài khoản của bạn đã bị khóa.", 404);
                }
                if (check == true)
                {
                    user = mapper.Map<UserLoginDto>(_user);
                    //check
                    // Console.WriteLine(user.RoleDto.RoleName);

                    string token = tokenConfiguration.CreateToken(user);
                    user.UserToken = token;
                    user.UserEmail = encryptConfiguration.Base64Decode(user.UserEmail);
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
                        throw;
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

                var currentTime = DateTime.UtcNow;
                var expiredTime = currentTime.AddSeconds(60);
                // Round down to the nearest minute

                var otp = totp.ComputeTotp(currentTime);

                string email = encryptConfiguration.Base64Encode(recipientEmail);

                var user = await context.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefaultAsync();

                switch (false)
                {
                    case var isExist when isExist == (user != null):
                        throw new MyException("Tài khoản không tồn tại.", 404);
                    case var isFalse when isFalse == (user!.EmailConfirmed != 1):
                        throw new MyException("Tài khoản này đã được xác thực.", 404);
                }

                user!.OTP = otp;
                user.ExpiredIn = expiredTime;

                await userRepository.Update(user);

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

        public async Task ValidateOTP(string otp, string recipientEmail)
        {
            string email = encryptConfiguration.Base64Encode(recipientEmail);

            var user = context.Users.Where(u => u.UserEmail.Equals(email)).FirstOrDefault();

            var currentTime = DateTime.UtcNow;

            switch (false)
            {
                case var isExist when isExist == (user != null):
                    throw new MyException("Tài khoản không tồn tại.", 404);
                case var isFalse when isFalse == (user!.OTP == otp):
                    throw new MyException("OTP của bạn không đúng.", 404);
                case var isFalse when isFalse == (currentTime < user.ExpiredIn):
                    throw new MyException("OTP đã hết hạn có thể xác thực.", 404);
            }

            user.EmailConfirmed = 1;

            await userRepository.Update(user);
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
                        throw;
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
                        throw;
                }
            }
        }

        public async Task UserRegister(UserCreateRequestDto requestDto)
        {
            try
            {
                var encodeEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail);
                var isEmailExist = await userRepository.IsEmailExist(encodeEmail);

                switch (false)
                {
                    case var isExist when isExist != isEmailExist:
                        throw new MyException("Tài khoản đăng kí của bạn đã toàn tại.", 404);
                    case var isExist when isExist == (requestDto.UserPassword.Equals(requestDto.PasswordConfirm)):
                        throw new MyException("Mật khẩu của bạn không khớp với mật khẩu xác nhận.", 404);
                }

                encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);
                var encryptEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail);

                var user = mapper.Map<UserCreateRequestDto, GraduationThesis_CarServices.Models.Entity.User>(requestDto,
                opt => opt.AfterMap((src, des) =>
                {
                    des.UserEmail = encryptEmail;
                    des.PasswordHash = password_hash;
                    des.PasswordSalt = password_salt;
                    des.UserStatus = Status.Activate;
                    des.EmailConfirmed = 0;
                    des.CreatedAt = DateTime.Now;
                    des.RoleId = 1;
                }));

                var userId = await userRepository.Create(user);

                var customer = new Models.Entity.Customer()
                {
                    User = user
                };

                await customerRepository.Create(customer);
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

        public async Task<string> IsEmailExist(string entityName)
        {
            try
            {
                var encodeEmail = encryptConfiguration.Base64Encode(entityName);
                var isEmailExist = await userRepository.IsEmailExist(encodeEmail);

                switch (false)
                {
                    case var isExist when isExist == isEmailExist:
                        throw new MyException("Tài khoản của bạn không tồn tại.", 404);
                }

                return entityName;
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

        public async Task ChangePassword(ChangePasswordDto requestDto)
        {
            try
            {
                var encodeEmail = encryptConfiguration.Base64Encode(requestDto.Email);
                var user = await userRepository.GetUserByEmail(encodeEmail);

                switch (false)
                {
                    case var isExist when isExist == (user != null):
                        throw new MyException("Tài khoản của bạn không tồn tại.", 404);
                    case var isExist when isExist == (requestDto.UserPassword.Equals(requestDto.PasswordConfirm)):
                        throw new MyException("Mật khẩu của bạn không khớp với mật khẩu xác nhận.", 404);
                }

                encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);

                user!.PasswordHash = password_hash;
                user.PasswordSalt = password_salt;

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

        public async Task<int> Count(string entityName)
        {
            try
            {
                return await EntityCountConfiguration<int>.Count(context, entityName);
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