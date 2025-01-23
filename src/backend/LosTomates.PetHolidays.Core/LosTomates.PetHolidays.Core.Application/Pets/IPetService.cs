namespace LosTomates.PetHolidays.Core.Application.Pets;

public interface IPetService
{
    Task<IReadOnlyList<PetView>> GetByUserId(string userId);
    Task<PetView> GetById(int petId);
    Task<int> Create(string userId, PetEditDto dto);
    Task Delete(int petId);
    Task Update(int petId, PetEditDto dto);
}