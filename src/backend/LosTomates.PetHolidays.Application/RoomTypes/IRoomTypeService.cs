namespace LosTomates.PetHolidays.Application.RoomTypes;

public interface IRoomTypeService
{
    Task<IReadOnlyList<RoomTypeView>> GetAll(int hotelId);

    Task<RoomTypeView> GetById(int hotelId, int entityId);

    Task<int> Create(int hotelId, RoomTypeEditDto dto);

    Task Update(int hotelId, int entityId, RoomTypeEditDto dto);

    Task Delete(int hotelId, int entityId);
}