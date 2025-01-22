namespace LosTomates.PetHolidays.Core.Application.Rooms;

public sealed record RoomEditDto
{
    public required string Name { get; init; }

    public string? Description { get; init; }

    public int RoomTypeId { get; init; }

    public string? Location { get; init; }

    public decimal Price { get; init; }
}

