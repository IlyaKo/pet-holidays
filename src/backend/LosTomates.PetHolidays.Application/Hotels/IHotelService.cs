
namespace LosTomates.PetHolidays.Application.Hotels;

public interface IHotelService
{
    Task<IReadOnlyList<HotelView>> GetAll();

    Task<HotelView?> GetById(int hotelId);

    Task<int> Create(HotelEditDto dto);

    Task Update(int entityId, HotelEditDto dto);

    Task Delete(int entityId);
}