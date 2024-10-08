using LosTomates.PetHolidays.Core.Domain.Hotels;

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
}
