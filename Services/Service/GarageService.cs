using System.Diagnostics;
using System.Globalization;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Models.DTO.Booking;
using GraduationThesis_CarServices.Models.DTO.Exception;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.Search;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class GarageService : IGarageService
    {
        private readonly IMapper mapper;
        private readonly IGarageRepository garageRepository;
        private readonly GeocoderConfiguration geocoderConfiguration;
        private readonly IBookingService bookingService;
        public GarageService(IMapper mapper, IGarageRepository garageRepository, GeocoderConfiguration geocoderConfiguration, IBookingService bookingService)
        {
            this.garageRepository = garageRepository;
            this.mapper = mapper;
            this.geocoderConfiguration = geocoderConfiguration;
            this.bookingService = bookingService;
        }

        public async Task<List<GarageAdminListResponseDto>> ViewAllForAdmin(PageDto page)
        {
            try
            {
                var list = await garageRepository.View(page);

                return mapper.Map<List<Garage>?, List<GarageAdminListResponseDto>>(list,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < des.Count; i++)
                    {
                        (var totalServices, var totalOrders) = garageRepository.GetServicesAndBookingsPerGarage(src![i].GarageId);
                        des[i].TotalOrders = totalOrders;
                        des[i].TotalServices = totalServices;
                        if (list![i].Reviews.Count != 0)
                        {
                            des[i].Rating = list[i].Reviews.Sum(r => r.Rating) / list[i].Reviews.Count;
                        }
                        des[i].GarageStatus = src![i].GarageStatus.ToString();
                    }
                }));
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

        public async Task<List<GarageListResponseDto>?> View(PageDto page)
        {

            try
            {
                var list = await garageRepository.View(page);

                return mapper.Map<List<Garage>?, List<GarageListResponseDto>>(list,
                otp => otp.AfterMap((src, des) =>
                {
                    for (int i = 0; i < des.Count; i++)
                    {
                        if (list![i].Reviews.Count != 0)
                        {
                            des[i].Rating = list[i].Reviews.Sum(r => r.Rating) / list[i].Reviews.Count;
                        }
                        des[i].GarageStatus = src![i].GarageStatus.ToString();
                    }
                }));
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

        public async Task<List<GarageListMobileMapResponseDto>> GetAllCoordinates()
        {
            try
            {
                var list = await garageRepository.GetAllCoordinates();

                var listDto = mapper.Map<List<GarageListMobileMapResponseDto>>(list);

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

        public async Task<List<GarageListResponseDto>?> Search(SearchDto search)
        {
            try
            {
                var list = await garageRepository.Search(search);

                return mapper.Map<List<Garage>?, List<GarageListResponseDto>>
                (list, opt => opt.AfterMap((src, des) =>
                {
                    for (int i = 0; i < list?.Count; i++)
                    {
                        if (list[i].Reviews.Count != 0)
                        {
                            des[i].Rating = list[i].Reviews.Sum(r => r.Rating) / list[i].Reviews.Count;
                        }
                        des[i].GarageStatus = src![i].GarageStatus.ToString();
                    }
                }));
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

        public async Task<GarageDetailResponseDto?> Detail(int id)
        {
            try
            {
                var garage = await garageRepository.Detail(id);

                switch (false)
                {
                    case var isExist when isExist == (garage != null):
                        throw new MyException("The garage doesn't exist.", 404);
                }

                return mapper.Map<GarageDetailResponseDto>(garage);
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

        public async Task Create(GarageCreateRequestDto requestDto)
        {
            try
            {
                var formatPhone = string.Empty;

                if (requestDto.GarageContactInformation is not null &&
                requestDto.GarageContactInformation.Length == 10)
                {
                    formatPhone = "+84" + requestDto.GarageContactInformation.Substring(1, 9);
                }

                var isExist = await garageRepository.IsGaragePhoneExist(formatPhone);
                var isAddressExist = await garageRepository.IsGarageAddressExist(requestDto.GarageAddress);
                int openAt, closeAt;

                if (string.IsNullOrEmpty(requestDto.OpenAt) &&
                string.IsNullOrEmpty(requestDto.CloseAt))
                {
                    throw new MyException("Giờ mở cửa và giờ đóng cửa không được để trống.", 404);
                }
                else
                {
                    openAt = DateTime.ParseExact("0" + requestDto.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                    closeAt = DateTime.ParseExact("0" + requestDto.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay.Hours;
                }

                switch (false)
                {
                    case var isFalse when isFalse == !string.IsNullOrEmpty(requestDto.GarageContactInformation):
                        throw new MyException("Số điện thoại không được để trống.", 404);
                    case var isFalse when isFalse == requestDto.GarageContactInformation!.All(char.IsDigit):
                        throw new MyException("Số điện thoại không được nhập kí tự khác ngoài chữ số.", 404);
                    case var isFalse when isFalse == (requestDto.GarageContactInformation!.Length == 10):
                        throw new MyException("Số điện thoại phải được nhập đủ 10 số.", 404);
                    case var isFalse when isFalse == (requestDto.GarageAddress!.Length <= 50):
                        throw new MyException("Địa chỉ không quá 50 kí tự.", 404);
                    case var isFalse when isFalse == (requestDto.GarageWard!.Length <= 50):
                        throw new MyException("Phường không quá 50 kí tự.", 404);
                    case var isFalse when isFalse == (requestDto.GarageDistrict!.Length <= 50):
                        throw new MyException("Quận không quá 50 kí tự.", 404);
                    case var isFalse when isFalse == (requestDto.GarageCity!.Length <= 50):
                        throw new MyException("Thành phố không quá 50 kí tự.", 404);
                    case var isFalse when isFalse != isExist:
                        throw new MyException("Số điện thoại đã tồn tại.", 404);
                    case var isFalse when isFalse != isAddressExist:
                        throw new MyException("Địa chỉ Garage đã tồn tại.", 404);
                    case var isFalse when isFalse == (openAt < closeAt):
                        throw new MyException("Giờ mở cửa không được phép nhỏ hơn giờ đóng cửa.", 404);
                }

                (double Latitude, double Longitude) = await geocoderConfiguration
                .GeocodeAsync(requestDto.GarageAddress, requestDto.GarageCity, requestDto.GarageDistrict, requestDto.GarageWard);

                var garage = mapper.Map<GarageCreateRequestDto, Garage>(requestDto,
                otp => otp.AfterMap((src, des) =>
                {
                    des.GarageStatus = Status.Activate;
                    des.CreatedAt = DateTime.Now;
                    des.GarageLatitude = Latitude;
                    des.GarageLongitude = Longitude;
                }));

                await garageRepository.Create(garage);
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

        public async Task Update(GarageUpdateRequestDto requestDto)
        {
            try
            {
                var g = await garageRepository.Detail(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == (g != null):
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var garage = mapper.Map<GarageUpdateRequestDto, Garage>(requestDto, g!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));

                await garageRepository.Update(garage);
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

        public async Task UpdateLocation(LocationUpdateRequestDto requestDto)
        {
            try
            {
                var g = await garageRepository.Detail(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == (g != null):
                        throw new MyException("The garage doesn't exist.", 404);
                }

                (double Latitude, double Longitude) = await geocoderConfiguration.GeocodeAsync(requestDto.GarageAddress, requestDto.GarageCity, requestDto.GarageDistrict, requestDto.GarageWard);

                var garage = mapper.Map<LocationUpdateRequestDto, Garage>(requestDto, g!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                    des.GarageLatitude = Latitude;
                    des.GarageLongitude = Longitude;
                }));

                await garageRepository.Update(garage);
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

        public async Task UpdateStatus(GarageStatusRequestDto requestDto)
        {
            try
            {
                var g = await garageRepository.Detail(requestDto.GarageId);

                switch (false)
                {
                    case var isExist when isExist == (g != null):
                        throw new MyException("The garage doesn't exist.", 404);
                }

                var garage = mapper.Map<GarageStatusRequestDto, Garage>(requestDto, g!);

                await garageRepository.Update(garage);
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
        public async Task<List<GarageListResponseDto>?> FilterGaragesByDateAndService(FilterGarageRequestDto requestDto)
        {
            try
            {
                var filteredGaragesByService = await garageRepository.GetGrageFilterByDateAndService(requestDto.ServiceList);

                foreach (var garage in filteredGaragesByService!.ToList())
                {
                    var checkBooking = new BookingCheckRequestDto()
                    {
                        DateSelected = requestDto.DateSelected,
                        GarageId = garage.GarageId
                    };

                    //Remove garage which is full on select date
                    var isFalse = bookingService.IsBookingAvailable(checkBooking).Result.All(g => g.IsAvailable is false);

                    if (isFalse)
                    {
                        filteredGaragesByService!.Remove(garage);
                    }
                }

                var filteredGarages = new List<Garage>();

                if (requestDto.Latitude is not null &&
                requestDto.Longitude is not null &&
                requestDto.RadiusInKm is not null)
                {
                    const double earthRadiusInKm = 6371.01;

                    foreach (var garage in filteredGaragesByService!)
                    {
                        double lat1 = Math.PI * requestDto.Latitude.Value / 180.0;
                        double lon1 = Math.PI * requestDto.Longitude.Value / 180.0;
                        double lat2 = Math.PI * garage.GarageLatitude / 180.0;
                        double lon2 = Math.PI * garage.GarageLongitude / 180.0;

                        double dlon = lon2 - lon1;
                        double dlat = lat2 - lat1;
                        var a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
                        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                        var distanceInKm = earthRadiusInKm * c;

                        if (distanceInKm <= requestDto.RadiusInKm)
                        {
                            filteredGarages.Add(garage);
                        }
                    }
                }

                return mapper.Map<List<GarageListResponseDto>>
                (filteredGarages, opt => opt.AfterMap((src, des) =>
                {
                    for (int i = 0; i < filteredGarages.Count; i++)
                    {
                        if (filteredGarages[i].Reviews.Count != 0)
                        {
                            des[i].Rating = filteredGarages[i].Reviews.Sum(r => r.Rating) / filteredGarages[i].Reviews.Count;
                        }
                    }
                }));
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

        public async Task<List<GarageListResponseDto>?> FilterGaragesNearMe(LocationRequestDto requestDto)
        {
            try
            {
                const double earthRadiusInKm = 6371.01;
                var unfilteredGarages = await garageRepository.GetAll();
                var filteredGarages = new List<Garage>();
                foreach (var garage in unfilteredGarages!)
                {
                    double lat1 = Math.PI * requestDto.Latitude / 180.0;
                    double lon1 = Math.PI * requestDto.Longitude / 180.0;
                    double lat2 = Math.PI * garage.GarageLatitude / 180.0;
                    double lon2 = Math.PI * garage.GarageLongitude / 180.0;

                    double dlon = lon2 - lon1;
                    double dlat = lat2 - lat1;
                    var a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
                    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    var distanceInKm = earthRadiusInKm * c;

                    if (distanceInKm <= requestDto.RadiusInKm)
                    {
                        filteredGarages.Add(garage);
                    }
                }
                return mapper.Map<List<GarageListResponseDto>>
                (filteredGarages, opt => opt.AfterMap((src, des) =>
                {
                    for (int i = 0; i < filteredGarages.Count; i++)
                    {
                        if (filteredGarages[i].Reviews.Count != 0)
                        {
                            des[i].Rating = filteredGarages[i].Reviews.Sum(r => r.Rating) / filteredGarages[i].Reviews.Count;
                        }
                    }
                }));
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

        public async Task<LotResponseDto> GetListLotByGarage(int garageId)
        {
            try
            {
                (var listObj, var countFree, var countBeingUsed) = await garageRepository.GetListLotByGarage(garageId);
                var listDto = mapper.Map<List<LotList>>(listObj);
                var lotResponse = new LotResponseDto { LotLists = listDto, FreeCount = countFree, BeingUsedCount = countBeingUsed };
                return lotResponse;
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

        public async Task<List<GetIdAndNameDto>> GetAllIdAndNameByGarage()
        {
            try
            {
                var list = await garageRepository.GetAll();

                var listDto = mapper.Map<List<GetIdAndNameDto>>(list);

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