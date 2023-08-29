using System.Diagnostics;
using AutoMapper;
using GraduationThesis_CarServices.Encrypting;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Paging;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly ICarRepository carRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IGarageRepository garageRepository;
        private readonly IMechanicRepository mechanicRepository;
        private readonly IAuthenticationRepository authenticationRepository;
        private readonly EncryptConfiguration encryptConfiguration;

        public UserService(IMapper mapper, IUserRepository userRepository, IGarageRepository garageRepository,
        EncryptConfiguration encryptConfiguration, ICarRepository carRepository, ICustomerRepository customerRepository,
        IMechanicRepository mechanicRepository, IAuthenticationRepository authenticationRepository)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.encryptConfiguration = encryptConfiguration;
            this.carRepository = carRepository;
            this.customerRepository = customerRepository;
            this.garageRepository = garageRepository;
            this.mechanicRepository = mechanicRepository;
            this.authenticationRepository = authenticationRepository;
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
                        throw;
                }
            }
        }

        public async Task<List<CustomerListResponseDto>> SearchCustomer(string search)
        {
            try
            {
                var list = mapper.Map<List<User>?, List<CustomerListResponseDto>>(await userRepository.SearchUser(search, 1),
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < src!.Count; i++)
                    {
                        des[i].TotalBooking = userRepository.TotalBooking(src[i].Customer.CustomerId);
                    }
                }));

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
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<CustomerListResponseDto>>> FilterCustomer(PageDto page)
        {
            try
            {
                (var listObj, var count) = await userRepository.FilterByRole(page, 1, 0);

                var listDto = mapper.Map<List<User>, List<CustomerListResponseDto>>(listObj!,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < src.Count; i++)
                    {
                        des[i].TotalBooking = userRepository.TotalBooking(src[i].Customer.CustomerId);
                    }
                }));

                var list = new GenericObject<List<CustomerListResponseDto>>(listDto, count);

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
                        throw;
                }
            }
        }

        public async Task<List<UserListResponseDto>?> SearchUser(string search, int roleId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == (roleId != 0):
                        throw new MyException("Can't accept value 0.", 404);
                }

                var list = mapper.Map<List<UserListResponseDto>>(await userRepository.SearchUser(search, roleId));
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
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<UserListResponseDto>>> FilterUser(PageDto page, int roleId, int garageId)
        {
            try
            {
                switch (false)
                {
                    case var isFalse when isFalse == (roleId != 0):
                        throw new MyException("Can't accept value 0.", 404);
                }

                (var listObj, var count) = await userRepository.FilterByRole(page, roleId, garageId);

                var listDto = mapper.Map<List<User>, List<UserListResponseDto>>(listObj!);

                var list = new GenericObject<List<UserListResponseDto>>(listDto, count);

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
                        throw;
                }
            }
        }

        public async Task<CustomerDetailResponseDto> CustomerDetail(int userId)
        {
            try
            {
                var c = await userRepository.CustomerDetail(userId);
                var customer = mapper.Map<CustomerDetailResponseDto>(c);

                return customer;
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

        public async Task<UserDetailResponseDto?> Detail(int id)
        {
            try
            {
                var user = await userRepository.Detail(id);

                switch (false)
                {
                    case var isExist when isExist == (user != null):
                        throw new MyException("The user doesn't exist.", 404);
                }

                switch (user)
                {
                    case var userCustomer when userCustomer!.Customer != null:
                        return mapper.Map<UserDetailResponseDto>(user);
                }

                return mapper.Map<UserDetailResponseDto>(user);
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

        public async Task CreateMechanic(MechanicCreateRequestDto requestDto, int? garageId)
        {
            try
            {
                if (garageId is not null)
                {
                    var encryptEmail = string.Empty;
                    var formatPhone = string.Empty;

                    if (requestDto.UserPhone is not null &&
                    requestDto.UserPhone.Length == 10)
                    {
                        formatPhone = "+84" + requestDto.UserPhone.Substring(1, 9);
                    }

                    var isPhoneExist = await userRepository.IsUserPhoneExist(formatPhone);

                    switch (false)
                    {
                        case var isFalse when isFalse == !isPhoneExist:
                            throw new MyException("Số điện thoại đã tồn tại.", 404);
                        case var isFalse when isFalse == requestDto.UserPassword.Equals(requestDto.PasswordConfirm):
                            throw new MyException("Mật khẩu xác nhận không khớp.", 404);
                        case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.UserPhone):
                            throw new MyException("Số điện thoại không được để trống.", 404);
                        case var isFalse when isFalse == requestDto.UserPhone!.All(char.IsDigit):
                            throw new MyException("Số điện thoại không được nhập kí tự khác ngoài chữ số.", 404);
                    }

                    if (requestDto.UserEmail is not null)
                    {
                        encryptEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail!);
                    }

                    if (await userRepository.IsEmailExist(encryptEmail))
                    {
                        throw new MyException("Tài khoản của bạn không tồn tại.", 404);
                    }

                    encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);

                    var user = mapper.Map<User>(requestDto,
                    opt => opt.AfterMap((src, des) =>
                    {
                        des.UserEmail = encryptEmail;
                        des.UserPhone = formatPhone;
                        des.PasswordHash = password_hash;
                        des.PasswordSalt = password_salt;
                        des.RoleId = 3;
                    }));

                    var userId = await userRepository.Create(user);

                    string level = string.Empty;

                    switch (requestDto.Level)
                    {
                        case 1:
                            level = MechanicLevel.Level1.ToString();
                            break;
                        case 2:
                            level = MechanicLevel.Level2.ToString();
                            break;
                        case 3:
                            level = MechanicLevel.Level3.ToString();
                            break;
                    }

                    var mechanic = new Mechanic() { Level = level, UserId = userId, MechanicStatus = MechanicStatus.Available };

                    var mechanicId = await mechanicRepository.Create(mechanic);

                    var garageMechanic = new GarageMechanic() { GarageId = garageId, MechanicId = mechanicId };

                    await garageRepository.CreateGarageMechanic(garageMechanic);
                }
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

        public async Task Create(UserCreateRequestDto requestDto, int? garageId)
        {
            try
            {
                var encryptEmail = string.Empty;
                var formatPhone = string.Empty;

                switch (false)
                {
                    case var isFalse when isFalse == (requestDto.RoleId > 0 && requestDto.RoleId <= 5):
                        throw new MyException("RoleId không tồn tại.", 404);
                    case var isFalse when isFalse == requestDto.UserPassword.Equals(requestDto.PasswordConfirm):
                        throw new MyException("Mật khẩu xác nhận không khớp.", 404);
                }

                if (requestDto.UserEmail is not null)
                {
                    encryptEmail = encryptConfiguration.Base64Encode(requestDto.UserEmail!);
                }

                encryptConfiguration.CreatePasswordHash(requestDto.UserPassword, out byte[] password_hash, out byte[] password_salt);

                if (requestDto.UserPhone is not null &&
                requestDto.UserPhone.Length == 10)
                {
                    formatPhone = "+84" + requestDto.UserPhone.Substring(1, 9);
                }

                switch (requestDto.RoleId)
                {
                    case 1:
                        var isUserPhoneExist = await userRepository.IsUserPhoneExist(formatPhone);
                        var testy = string.IsNullOrEmpty(formatPhone);
                        var asdasd = isUserPhoneExist && string.IsNullOrEmpty(formatPhone);
                        var asdasdasd = isUserPhoneExist && !string.IsNullOrEmpty(formatPhone);
                        var asdasd1 = !isUserPhoneExist && !string.IsNullOrEmpty(formatPhone);

                        switch (false)
                        {
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.UserPhone):
                                throw new MyException("Số điện thoại không được để trống.", 404);
                            case var isFalse when isFalse == requestDto.UserPhone!.All(char.IsDigit):
                                throw new MyException("Số điện thoại không được nhập kí tự khác ngoài chữ số.", 404);
                            case var isFalse when isFalse == (requestDto.UserPhone!.Length == 10):
                                throw new MyException("Số điện thoại phải được nhập đủ 10 số.", 404);
                            case var isFalse when isFalse == !isUserPhoneExist:
                                throw new MyException("Số điện thoại đã tồn tại.", 404);
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.CarModel):
                                throw new MyException("Mẫu xe không được để trống.", 404);
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.CarBrand):
                                throw new MyException("Thương hiệu xe không được để trống.", 404);
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.CarLicensePlate):
                                throw new MyException("Biển số xe không được để trống.", 404);
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.CarFuelType):
                                throw new MyException("Loại nhiên liệu xe không được để trống.", 404);
                            case var isFalse when isFalse == requestDto.NumberOfCarLot is not null:
                                throw new MyException("Số ghế xe không được để trống.", 404);
                        }

                        var customer = mapper.Map<User>(requestDto,
                        opt => opt.AfterMap((src, des) =>
                        {
                            des.UserPhone = formatPhone;
                            des.UserEmail = encryptEmail;
                            des.PasswordHash = password_hash;
                            des.PasswordSalt = password_salt;
                            des.RoleId = requestDto.RoleId;
                        }));

                        await userRepository.Create(customer);

                        var customer_ = new Customer() { User = customer };

                        var customerId = await customerRepository.Create(customer_);

                        var car = mapper.Map<UserCreateRequestDto, Car>(requestDto,
                        obj => obj.AfterMap((src, des) =>
                        {
                            des.CustomerId = customerId;
                        }));

                        await carRepository.Create(car);
                        break;
                    case 2:
                        if (await userRepository.IsEmailExist(encryptEmail))
                        {
                            throw new MyException("Tài khoản của bạn không tồn tại.", 404);
                        }

                        if (garageId is null)
                        {
                            throw new MyException("Chỉ có Admin là được tạo Manager.", 404);
                        }

                        if (string.IsNullOrEmpty(requestDto.UserEmail))
                        {
                            throw new MyException("Email không được để trống.", 404);
                        }

                        var manager = mapper.Map<User>(requestDto,
                        opt => opt.AfterMap((src, des) =>
                        {
                            des.UserEmail = encryptEmail;
                            des.PasswordHash = password_hash;
                            des.PasswordSalt = password_salt;
                            des.RoleId = requestDto.RoleId;
                        }));

                        await userRepository.Create(manager);
                        break;
                    case 5:
                        var isUserPhoneExist_ = await userRepository.IsUserPhoneExist(formatPhone);

                        if (isUserPhoneExist_)
                        {
                            throw new MyException("Số điện thoại đã tồn tại.", 404);
                        }

                        switch (false)
                        {
                            case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.UserPhone):
                                throw new MyException("Số điện thoại không được để trống.", 404);
                            case var isFalse when isFalse == requestDto.UserPhone!.All(char.IsDigit):
                                throw new MyException("Số điện thoại không được nhập kí tự khác ngoài chữ số.", 404);
                            case var isFalse when isFalse == (requestDto.UserPhone!.Length == 10):
                                throw new MyException("Số điện thoại phải được nhập đủ 10 số.", 404);
                        }

                        var staff = mapper.Map<User>(requestDto,
                        opt => opt.AfterMap((src, des) =>
                        {
                            des.UserPhone = formatPhone;
                            des.UserEmail = encryptEmail;
                            des.PasswordHash = password_hash;
                            des.PasswordSalt = password_salt;
                            des.RoleId = requestDto.RoleId;
                        }));

                        if (garageId is not null)
                        {
                            var managerId = await garageRepository.GetManagerId(garageId);
                            staff.ManagerId = managerId;
                            await userRepository.Create(staff);
                        }
                        else
                        {
                            throw new MyException("Chỉ có Manager là được tạo Staff.", 404);
                        }
                        break;
                }
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

        public async Task<string> CustomerFirstLoginUpdate(UserUpdateRequestDto requestDto, int userId)
        {
            try
            {
                var u = await userRepository.Detail(userId);
                var refreshToken = string.Empty;

                switch (false)
                {
                    case var isFalse when isFalse != requestDto.UserFirstName.Equals(""):
                        throw new MyException("Tên đầu của người dùng không được để trống.", 404);
                    case var isFalse when isFalse != requestDto.UserLastName!.Equals(""):
                        throw new MyException("Tên cuối của người dùng không được để trống.", 404);
                    case var isExist when isExist == (u is not null):
                        throw new MyException("Người dùng không tồn tại.", 404);
                    case var isCustomer when isCustomer == (u!.RoleId == 1 || u!.RoleId == 5):
                        throw new MyException("Authorized.", 403);
                    case var isFirstLogin when isFirstLogin != (u.UserFirstName is not null):
                        var newRefreshToken = await authenticationRepository.RefreshToken(u.UserId);
                        refreshToken = newRefreshToken!.Token;
                        break;
                }

                var user = mapper.Map<UserUpdateRequestDto, User>(requestDto, u!,
                opt => opt.AfterMap((src, des) =>
                {
                    if (requestDto.UserEmail is not null)
                    {
                        des.UserEmail = encryptConfiguration.Base64Encode(src.UserEmail!);
                    }
                    des.UpdatedAt = DateTime.Now;
                }));
                await userRepository.Update(user);
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
                        throw;
                }
            }
        }

        public async Task<GenericObject<List<UserListResponseDto>>> GetStaffByGarage(PagingBookingPerGarageRequestDto requestDto)
        {
            try
            {
                var isGarageExist = await garageRepository.IsGarageExist(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == isGarageExist:
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var page = new PageDto { PageIndex = requestDto.PageIndex, PageSize = requestDto.PageSize };

                (var listObj, var count) = await userRepository.GetStaffByGarage(page, requestDto.GarageId);

                var listDto = mapper.Map<List<UserListResponseDto>>(listObj);

                var list = new GenericObject<List<UserListResponseDto>>(listDto, count);

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
                        throw;
                }
            }
        }

        public async Task<List<GetIdAndNameDto>> GetManagerNotAssignByGarage()
        {
            try
            {
                var listObj = await userRepository.GetManagerNotAssignByGarage();

                var listDto = mapper.Map<List<GetIdAndNameDto>>(listObj);

                return listDto;
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
    }
}