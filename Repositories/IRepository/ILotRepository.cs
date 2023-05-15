using GraduationThesis_CarServices.Models.Entity;

namespace GraduationThesis_CarServices.Repositories.IRepository
{
    public interface ILotRepository
    {
        Task<List<Lot>?> GetAllLotInGarage(int garageId);
        Task<Lot?> IsFree();
        Task Update(Lot lot);
    }
}