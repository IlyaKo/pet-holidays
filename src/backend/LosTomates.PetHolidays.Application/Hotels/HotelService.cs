using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelService : IHotelService
{
    public async Task<IReadOnlyList<HotelView>> GetAll()
        => await Task.FromResult(FakeData.Hotels.ConvertAll(x => new HotelView(x)));
}
