using LosTomates.PetHolidays.Core.Domain.Hotels;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelView
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
