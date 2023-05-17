using System.Globalization;
using AutoMapper;
using GraduationThesis_CarServices.Enum;
using GraduationThesis_CarServices.Geocoder;
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
        public GarageService(IMapper mapper, IGarageRepository garageRepository, GeocoderConfiguration geocoderConfiguration)
        {
            this.garageRepository = garageRepository;
            this.mapper = mapper;
            this.geocoderConfiguration = geocoderConfiguration;
        }

        public async Task<List<GarageListResponseDto>?> View(PageDto page)
        {

            try
            {
                var list = mapper
                .Map<List<GarageListResponseDto>>(await garageRepository.View(page));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GarageListResponseDto>?> Search(SearchDto search)
        {
            try
            {
                var list = await garageRepository.Search(search);
                return mapper.Map<List<GarageListResponseDto>>
                (list, opt => opt.AfterMap((src, des) =>
                {
                    for (int i = 0; i < list?.Count; i++)
                    {
                        if (list[i].Reviews.Count != 0)
                        {
                            des[i].Rating = list[i].Reviews.Sum(r => r.Rating) / list[i].Reviews.Count;
                        }
                    }
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GarageDetailResponseDto?> Detail(int id)
        {
            try
            {
                var garage = mapper
                .Map<Garage?, GarageDetailResponseDto>(await garageRepository.Detail(id),
                otp => otp.AfterMap((src, des) =>
                {
                    des.HoursOfOperation = "From " + src!.OpenAt + " to " + src.CloseAt;
                    
                    var presentTime = DateTime.Now.TimeOfDay;
                    var openAt = DateTime.ParseExact(src.OpenAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var closeAt = DateTime.ParseExact(src.CloseAt, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var midnight = TimeSpan.Zero;

                    switch (presentTime)
                    {
                        case var time when TimeSpan.Compare(time, openAt).Equals(1) && TimeSpan.Compare(time, closeAt).Equals(-1):

                            des.IsOpen = "Open";
                            break;
                        case var time when TimeSpan.Compare(time, closeAt).Equals(1) || TimeSpan.Compare(midnight, openAt).Equals(-1):

                            des.IsOpen = "Closed";
                            break;
                    }
                    switch (src.Lots.All(l => l.LotStatus.Equals(LotStatus.Busy))){
                        case var source when source == true:
                            des.IsFull = "Full";
                            break;
                        case var source when source == false:
                            des.IsFull = "Free";
                            break;
                    }
                }));
                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(GarageCreateRequestDto requestDto)
        {
            try
            {
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
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(GarageUpdateRequestDto requestDto)
        {
            try
            {
                var g = await garageRepository.Detail(requestDto.GarageId);
                var garage = mapper.Map<GarageUpdateRequestDto, Garage>(requestDto, g!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                }));
                await garageRepository.Update(garage);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateLocation(LocationUpdateRequestDto requestDto)
        {
            try
            {
                (double Latitude, double Longitude) = await geocoderConfiguration
                .GeocodeAsync(requestDto.GarageAddress, requestDto.GarageCity, requestDto.GarageDistrict, requestDto.GarageWard);
                var g = await garageRepository.Detail(requestDto.GarageId);
                var garage = mapper.Map<LocationUpdateRequestDto, Garage>(requestDto, g!,
                otp => otp.AfterMap((src, des) =>
                {
                    des.UpdatedAt = DateTime.Now;
                    des.GarageLatitude = Latitude;
                    des.GarageLongitude = Longitude;
                }));
                await garageRepository.Update(garage);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateStatus(GarageStatusRequestDto requestDto)
        {
            try
            {
                var g = await garageRepository.Detail(requestDto.GarageId);
                var garage = mapper.Map<GarageStatusRequestDto, Garage>(requestDto, g!);
                await garageRepository.Update(garage);
                return true;
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GarageListResponseDto>?> FilterGaragesWithCoupon(PageDto page)
        {
            var list = await garageRepository.FilterCoupon(page);
            return mapper.Map<List<GarageListResponseDto>>
                (list, opt => opt.AfterMap((src, des) =>
                {
                    for (int i = 0; i < list?.Count; i++)
                    {
                        if (list[i].Reviews.Count != 0)
                        {
                            des[i].Rating = list[i].Reviews.Sum(r => r.Rating) / list[i].Reviews.Count;
                        }
                    }
                }));
        }
    }
}