using AutoMapper;
using GraduationThesis_CarServices.Geocoder;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
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

        public async Task<GarageDetailResponseDto?> Detail(int id)
        {
            try
            {
                var garage = mapper
                .Map<GarageDetailResponseDto>(await garageRepository.Detail(id));
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
                    des.GarageStatus = 1;
                    des.CreatedAt = DateTime.Now;
                    des.Latitude = Latitude;
                    des.Longitude = Longitude;
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
                    des.Latitude = Latitude;
                    des.Longitude = Longitude;
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
                    double lat2 = Math.PI * garage.Latitude / 180.0;
                    double lon2 = Math.PI * garage.Longitude / 180.0;

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
                return mapper.Map<List<GarageListResponseDto>>(filteredGarages);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}