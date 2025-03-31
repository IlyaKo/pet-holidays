using LosTomates.PetHolidays.Auth.Core.Domain.Users;

namespace LosTomates.PetHolidays.Auth.DataAccess.DataSeed;

public static class FakeData
{
    public static List<User> Users { get; } =
    [
        new()
        {
            Id = "b10deb6c-63cb-4d73-8cbd-65203a5000db",
            UserName = "Test1",
            Email = "Test1@test.com",
            PhoneNumber = "+79296224167"
        },
        new()
        {
            Id = "1fb17ad5-f507-4683-8be7-4fe276d1f086",
            UserName = "Test2",
            Email = "Test2@test.com",
            PhoneNumber = "+79296224168"
        }
    ];
}