namespace LosTomates.PetHolidays.Application.PetTypes;

public interface IPetTypeService
{
    Task<IReadOnlyList<PetTypeView>> GetAll();

    Task<PetTypeView?> GetById(int hotelId);

    Task<int> Create(PetTypeEditDto dto);

    Task Update(int entityId, PetTypeEditDto dto);

    Task Delete(int entityId);
}

