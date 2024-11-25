namespace LosTomates.PetHolidays.Core.Domain;

public class PetType : BaseEntity
{
    public required string Name { get; set; }

    public bool IsActive { get; set; }
}

