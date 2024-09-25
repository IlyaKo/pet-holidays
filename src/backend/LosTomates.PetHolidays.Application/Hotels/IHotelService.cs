
namespace LosTomates.PetHolidays.Application.Hotels;

public interface IHotelService
{
    Task<IReadOnlyList<HotelView>> GetAll();
}