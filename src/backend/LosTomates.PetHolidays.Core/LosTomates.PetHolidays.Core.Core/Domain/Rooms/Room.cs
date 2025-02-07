using LosTomates.PetHolidays.Core.Core.Domain.Hotels;

namespace LosTomates.PetHolidays.Core.Core.Domain.Rooms;

public sealed class Room
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public Hotel? Hotel { get; set; }

    public required string Name { get; set; }

    public int RoomTypeId { get; set; }

    public RoomType? RoomType { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? PhotoUrl { get; set; }
}
