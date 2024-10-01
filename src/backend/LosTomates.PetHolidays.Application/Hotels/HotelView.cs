using LosTomates.PetHolidays.Core.Domain.Hotels;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelView
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public HotelView(Hotel source)
    {
        Id = source.Id;
        Name = source.Name;
        Description = source.Description;
        IsActive = source.IsActive;
    }
}
