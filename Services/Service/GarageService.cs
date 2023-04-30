using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Models.DTO.User;
using GraduationThesis_CarServices.Models.Entity;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class GarageService : IGarageService
    {
        private readonly IMapper mapper;
        private readonly IGarageRepository garageRepository;
        public GarageService(IMapper mapper, IGarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
            this.mapper = mapper;
        }

        public async Task<List<GarageDto>?> View(PageDto page)
        {

            try
            {
                List<GarageDto>? list = await garageRepository.View(page);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GarageDto>> GetGarageNearUser(UserLocationRequestDto requestDto)
        {
            try
            {
                User user = mapper.Map<User>(requestDto);
                List<GarageDto>? list = mapper.Map<List<GarageDto>>(await garageRepository.GetGarageNearUser(user));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GarageDto?> Detail(int id)
        {
            try
            {
                GarageDto? garage = mapper.Map<GarageDto>(await garageRepository.Detail(id));
                return garage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Create(CreateGarageDto createGarageDto)
        {
            try
            {
                await garageRepository.Create(createGarageDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(UpdateGarageDto updateGarageDto)
        {
            try
            {
                await garageRepository.Update(updateGarageDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(DeleteGarageDto deleteGarageDto)
        {
            try
            {
                await garageRepository.Delete(deleteGarageDto);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}