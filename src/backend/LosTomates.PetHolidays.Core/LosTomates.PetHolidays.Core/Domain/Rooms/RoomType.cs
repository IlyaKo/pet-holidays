namespace LosTomates.PetHolidays.Core.Domain.Rooms;

public sealed class RoomType
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public List<Room>? Rooms { get; set; }
}
