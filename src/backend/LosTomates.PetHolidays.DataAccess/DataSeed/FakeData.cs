using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Users;

namespace LosTomates.PetHolidays.DataAccess.DataSeed;

public static class FakeData
{
    public static List<Hotel> Hotels { get; } =
    [
        new()
        {
            Id = 1,
            Name = "Fluffy Inn",
            IsActive = true,
            Description = "A cozy and comfortable stay for pets of all sizes. With personalized care, fun activities, and a safe environment, your pets will enjoy their vacation as much as you do."
        },
        new()
        {
            Id = 2,
            Name = "Animals GuestHouse",
            IsActive = true,
            Description = "A royal treatment for your pets with spacious rooms, gourmet dining options, and a variety of activities. Our goal is to make every pet feel like royalty during their stay."
        },
        new()
        {
            Id = 3,
            Name = "New Hotel",
            IsActive = false,
            Description = "Still in development"
        },
        new()
        {
            Id = 4,
            Name = "Paws & Whiskers Retreat",
            IsActive = true,
            Description = "A luxurious getaway for your furry friends, offering spacious suites, gourmet meals, and plenty of playtime. Our dedicated staff ensures every guest feels right at home."
        },

    ];

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
