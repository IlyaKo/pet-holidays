using LosTomates.PetHolidays.Core.Domain.Hotels;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelView
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public HotelView(Hotel source)
    {
        Id = source.Id;
        Name = source.Name;
    }
}
