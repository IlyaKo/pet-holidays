namespace LosTomates.PetHolidays.Core.Domain.Pets;

public class PetType : BaseEntity
{
    public required string Name { get; set; }

    public bool IsActive { get; set; }
}

