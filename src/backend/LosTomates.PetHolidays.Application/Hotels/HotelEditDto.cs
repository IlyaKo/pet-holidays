namespace LosTomates.PetHolidays.Application.Hotels;

public sealed record HotelEditDto
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }
}

