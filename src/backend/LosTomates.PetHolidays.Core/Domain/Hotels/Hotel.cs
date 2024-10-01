namespace LosTomates.PetHolidays.Core.Domain.Hotels;

public sealed class Hotel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
