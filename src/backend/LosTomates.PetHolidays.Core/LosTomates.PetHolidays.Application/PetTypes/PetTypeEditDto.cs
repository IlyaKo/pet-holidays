namespace LosTomates.PetHolidays.Application.PetTypes;

public sealed record PetTypeEditDto
{
    public required string Name { get; init; }

    public bool IsActive { get; init; }
}

