using LosTomates.PetHolidays.Core.Domain.Hotels;

namespace LosTomates.PetHolidays.DataAccess;

public static class FakeData
{
    public static List<Hotel> Hotels { get; } =
    [
        new()
        {
            Id = 1,
            Name = "Fluffy Inn",
        },
        new()
        {
            Id = 2,
            Name = "Animals GuestHouse",
        }
    ];
}
