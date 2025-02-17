using LosTomates.PetHolidays.Core.Application.RoomTypes;

namespace LosTomates.PetHolidays.Core.Application.Rooms;

public sealed class RoomView
{
    public int Id { get; set; }

    public required RoomTypeView RoomType { get; set; }

    public required string Name { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PhotoUrl { get; set; }
}
