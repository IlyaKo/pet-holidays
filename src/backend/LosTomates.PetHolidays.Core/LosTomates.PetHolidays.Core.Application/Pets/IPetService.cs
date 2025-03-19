namespace LosTomates.PetHolidays.Core.Application.Pets;

public interface IPetService
{
    Task<IReadOnlyList<PetView>> GetByUserId(string userId);
    Task<PetView> GetById(int petId, string userId);
    Task<int> Create(string userId, PetEditDto dto);
    Task Delete(int petId, string userId);
    Task Update(int petId, string userId, PetEditDto dto);
}