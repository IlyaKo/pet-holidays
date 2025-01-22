using LosTomates.PetHolidays.Core.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Core.Domain.Users;

namespace LosTomates.PetHolidays.Core.DataAccess.DataSeed;

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

    public static List<RoomType> RoomTypes { get; } =
    [
        new()
        {
            Id = 1,
            Name = "Standart",
            Description = "A cozy haven with all the essentials, including a comfy bed, fresh water, and toys to keep your pet entertained. Perfect for pets who enjoy a no-frills, relaxing stay."
        },
        new()
        {
            Id = 2,
            Name = "Deluxe",
            Description = "A larger room with upgraded bedding and toys."
        },
    ];

    public static List<Room> Rooms { get; } =
    [
        new()
        {
            Id = 1,
            HotelId = 1,
            RoomTypeId = 1,
            Name = "Classic Stay",
            Description = "Simple yet comfortable accommodations with all the essentials, including a good bed, and water.",
            Location = "Second floor",
            Price = 10
        },
        new()
        {
            Id = 2,
            HotelId = 2,
            RoomTypeId = 1,
            Name = "Basic Retreat",
            Description = "A functional and clean environment with a comfortable sleeping area to ensure your pet feels at home.",
            Location = "Second floor",
            Price = 8
        },
        new()
        {
            Id = 3,
            HotelId = 3,
            RoomTypeId = 1,
            Name = "Classic Stay",
            Description = "Simple yet comfortable accommodations with all the essentials, including a good bed, and water.",
            Location = "Second floor",
            Price = 10
        },
        new()
        {
            Id = 4,
            HotelId = 4,
            RoomTypeId = 1,
            Name = "Classic Stay",
            Description = "Simple yet comfortable accommodations with all the essentials, including a good bed, and water.",
            Location = "Second floor",
            Price = 10
        },
        new()
        {
            Id = 5,
            HotelId = 1,
            RoomTypeId = 2,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
        },
        new()
        {
            Id = 6,
            HotelId = 2,
            RoomTypeId = 2,
            Name = "Premium Retreat",
            Description = "A spacious room with high-end bedding, extra toys, and premium treats, offering an elevated experience for your pet.",
            Location = "First floor",
            Price = 14
        },
        new()
        {
            Id = 7,
            HotelId = 3,
            RoomTypeId = 2,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
        },
        new()
        {
            Id = 8,
            HotelId = 4,
            RoomTypeId = 2,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
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

    public static List<PetType> PetTypes { get; } =
    [
        new()
        {
            Id = 1,
            Name = "Dog",
            IsActive= true,
        },
        new()
        {
            Id = 2,
            Name = "Cat",
             IsActive= true,
        },
        new()
        {
            Id = 3,
            Name = "Hamster",
            IsActive= true,
        },
        new()
        {
            Id = 4,
            Name = "Rabbit",
            IsActive= true,
        },
        new()
        {
            Id = 5,
            Name = "Parrot",
            IsActive= true,
        },
        new()
        {
            Id = 6,
            Name = "Turtle",
            IsActive= true,
        }
    ];
    public static List<Pet> Pets { get; } =
    [
        new()
        {
            Id = 1,
            Name = "Rex",
            PetTypeId=1,
            PetOwnerId="b10deb6c-63cb-4d73-8cbd-65203a5000db"
        },
        new()
        {
            Id = 2,
            Name = "Simba",
            PetTypeId=2,
            PetOwnerId="b10deb6c-63cb-4d73-8cbd-65203a5000db"
        },
        new()
        {
            Id = 3,
            Name = "Sharik",
            PetTypeId=1,
            PetOwnerId="1fb17ad5-f507-4683-8be7-4fe276d1f086"
        },
        new()
        {
            Id = 4,
            Name = "Rio",
            PetTypeId=5,
            PetOwnerId="1fb17ad5-f507-4683-8be7-4fe276d1f086"
        }
    ];
}
