using LosTomates.PetHolidays.Application.PetTypes;

namespace LosTomates.PetHolidays.Application.Pets;

public class PetView
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required PetTypeView PetType { get; set; }
}