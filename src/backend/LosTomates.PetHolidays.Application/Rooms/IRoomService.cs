
namespace LosTomates.PetHolidays.Application.Rooms;

public interface IRoomService
{
    Task<int> Create(int hotelId, RoomEditDto dto);
    Task Delete(int hotelId, int entityId);
    Task<IReadOnlyList<RoomView>> GetAll(int hotelId);
    Task<RoomView?> GetById(int hotelId, int entityId);
    Task Update(int hotelId, int entityId, RoomEditDto dto);
}