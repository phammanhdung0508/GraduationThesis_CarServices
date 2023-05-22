using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ILotRepository
    {
        Task<Lot> GetFreeLotInGarage(int garageId);
        Task<Lot> GetLotByLicensePlate(int garageId, string licensePlate);
        Task Update(Lot lot);
    }
}