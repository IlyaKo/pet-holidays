namespace LosTomates.PetHolidays.Core.Application.Hotels;

public sealed record HotelEditDto
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }

    public string? PhotoUrl { get; set; }
}

