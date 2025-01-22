using LosTomates.PetHolidays.Core.Core.Domain.Users;

namespace LosTomates.PetHolidays.Core.Core.Domain.Pets;
public class Pet : BaseEntity
{
    public string? Name { get; set; }

    public int PetTypeId { get; set; }

    public required string PetOwnerId { get; set; }

    public PetType? PetType { get; set; } 

    public User? PetOwner { get; set; }
}
