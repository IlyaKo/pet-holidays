using LosTomates.PetHolidays.Core.Domain.Users;

namespace LosTomates.PetHolidays.Core.Domain.Pets;
public class Pet : BaseEntity
{
    public string? Name { get; set; }

    public int PetTypeId { get; set; }

    public int PetOwnerId { get; set; }

    public PetType? PetType { get; set; } 

    public User? PetOwner { get; set; }
}
