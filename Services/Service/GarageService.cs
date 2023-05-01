using AutoMapper;
using GraduationThesis_CarServices.Models.DTO.Garage;
using GraduationThesis_CarServices.Models.DTO.Page;
using GraduationThesis_CarServices.Repositories.IRepository;
using GraduationThesis_CarServices.Services.IService;

namespace GraduationThesis_CarServices.Services.Service
{
    public class GarageService : IGarageService
    {
        private readonly IMapper mapper;
        private readonly IGarageRepository garageRepository;
        private readonly IUserRepository userRepository;

        public GarageService(IMapper mapper, IGarageRepository garageRepository, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.garageRepository = garageRepository;
            this.userRepository = userRepository;
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

        public async Task<GarageDto?> Detail(int id)
        {
            try
            {
                GarageDto? garage = await garageRepository.Detail(id);
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
                await userRepository.Detail(createGarageDto.UserId);

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