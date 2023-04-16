using AutoMapper;
using CarServices.Encrypting;
using CarServices.Models;
using CarServices.Models.DTO;
using CarServices.Models.Entity;
using CarServices.Repositories.IRepository;

namespace CarServices.Repositories.Repository.Authentication
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private EncryptConfiguration _encryptConfiguration;
        private TokenConfiguration _tokenConfiguration;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthenticationRepository(EncryptConfiguration encryptConfiguration, DataContext context, IMapper mapper, TokenConfiguration tokenConfiguration)
        {
            _encryptConfiguration = encryptConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserDto?> CheckLogin(LoginDto login)
        {
            try
            {
                UserDto? user = null;
                bool check = true;
                string email = _encryptConfiguration.Base64Decode(login.user_email);
                var _user = await _context.Users.Include(r => r.Role)
                .FirstOrDefaultAsync(u => u.user_email == email);
                if (_user == null)
                {
                    check = false;
                    //throw 404
                }
                else
                {
                    if (!VerifyPasswordHash(login.user_password, _user.password_hash, _user.password_salt))
                    {
                        check = false;
                        //throw 404
                    }
                }
                if (_user.user_status == false)
                {
                    check = false;
                    //throw 404
                }
                if (check == true)
                {
                    user = _mapper.Map<UserDto>(_user);
                    //check
                    Console.WriteLine(user.roleDto.role_name);

                    string token = _tokenConfiguration.CreateToken(user);
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

        public async Task CreateUser(CreateUserDto _user){
            try{
                _encryptConfiguration.CreatePasswordHash(_user.user_password, out byte[] password_hash, out byte[] password_salt);
                User user = new User{
                    user_email = _user.user_email,
                    password_hash = password_hash,
                    password_salt = password_salt
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}