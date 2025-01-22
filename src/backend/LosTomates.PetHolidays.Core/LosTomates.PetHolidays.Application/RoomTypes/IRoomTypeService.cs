namespace LosTomates.PetHolidays.Application.RoomTypes;

public interface IRoomTypeService
{
    Task<IReadOnlyList<RoomTypeView>> GetAll();

    Task<RoomTypeView> GetById(int entityId);

    Task<int> Create(RoomTypeEditDto dto);

    Task Update(int entityId, RoomTypeEditDto dto);

    Task Delete(int entityId);
}