namespace LosTomates.PetHolidays.Core.Application.Pets;

public sealed record PetEditDto(string? Name, int PetTypeId, string? PhotoUrl);