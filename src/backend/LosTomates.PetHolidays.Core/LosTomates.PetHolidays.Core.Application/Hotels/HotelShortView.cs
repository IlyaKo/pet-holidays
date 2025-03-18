namespace LosTomates.PetHolidays.Core.Application.Hotels;

public class HotelShortView
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? PhotoUrl { get; set; }
}
