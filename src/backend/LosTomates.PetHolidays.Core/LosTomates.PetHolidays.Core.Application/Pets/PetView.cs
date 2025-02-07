using LosTomates.PetHolidays.Core.Application.PetTypes;

namespace LosTomates.PetHolidays.Core.Application.Pets;

public class PetView
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required PetTypeView PetType { get; set; }
    public string? PhotoUrl { get; set; }
}