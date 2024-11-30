namespace LosTomates.PetHolidays.Application.RoomTypes;

public sealed class RoomTypeView
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}
