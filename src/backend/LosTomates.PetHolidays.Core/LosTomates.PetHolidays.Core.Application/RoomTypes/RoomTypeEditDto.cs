namespace LosTomates.PetHolidays.Core.Application.RoomTypes;

public sealed record RoomTypeEditDto
{
    public required string Name { get; init; }

    public string? Description { get; init; }
}
