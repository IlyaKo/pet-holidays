namespace LosTomates.PetHolidays.Core.Core.Exchange;

public sealed record EntityDeletedEventDto
{
    public required string Id { get; set; }

    public required string Type { get; set; }
}
