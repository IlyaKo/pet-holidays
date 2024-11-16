using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Rooms;

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

    public static List<RoomType> RoomTypes { get; } =
    [
        new()
        {
            Id = 1,
            HotelId = 1,
            Name = "Standart",
            Description = "A cozy haven with all the essentials, including a comfy bed, fresh water, and toys to keep your pet entertained. Perfect for pets who enjoy a no-frills, relaxing stay."
        },
        new()
        {
            Id = 2,
            HotelId = 2,
            Name = "Standart",
            Description = "Comfortable spaces with basic amenities"
        },
        new()
        {
            Id = 3,
            HotelId = 3,
            Name = "Standart",
            Description = "Comfortable spaces with basic amenities"
        },
        new()
        {
            Id = 4,
            HotelId = 4,
            Name = "Standart",
            Description = "Comfortable spaces with basic amenities"
        },
        new()
        {
            Id = 5,
            HotelId = 1,
            Name = "Deluxe",
            Description = "A larger room with upgraded bedding and toys."
        },
        new()
        {
            Id = 6,
            HotelId = 2,
            Name = "Deluxe",
            Description = "A step up from the standard, featuring a plush bed, extra space to stretch out, and a selection of premium toys and treats. Ideal for pets who enjoy a bit more pampering during their stay."
        },
        new()
        {
            Id = 7,
            HotelId = 3,
            Name = "Deluxe",
            Description = "A larger room with upgraded bedding and toys."
        },
        new()
        {
            Id = 8,
            HotelId = 4,
            Name = "Deluxe",
            Description = "Luxurious accommodations with spacious living areas, premium bedding, and a private play area. Equipped with entertainment options and top-tier treats, this room is perfect for pets who deserve the finest in comfort and care."
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
            RoomTypeId = 2,
            Name = "Basic Retreat",
            Description = "A functional and clean environment with a comfortable sleeping area to ensure your pet feels at home.",
            Location = "Second floor",
            Price = 8
        },
        new()
        {
            Id = 3,
            HotelId = 3,
            RoomTypeId = 3,
            Name = "Classic Stay",
            Description = "Simple yet comfortable accommodations with all the essentials, including a good bed, and water.",
            Location = "Second floor",
            Price = 10
        },
        new()
        {
            Id = 4,
            HotelId = 4,
            RoomTypeId = 4,
            Name = "Classic Stay",
            Description = "Simple yet comfortable accommodations with all the essentials, including a good bed, and water.",
            Location = "Second floor",
            Price = 10
        },
        new()
        {
            Id = 5,
            HotelId = 1,
            RoomTypeId = 5,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
        },
        new()
        {
            Id = 6,
            HotelId = 2,
            RoomTypeId = 6,
            Name = "Premium Retreat",
            Description = "A spacious room with high-end bedding, extra toys, and premium treats, offering an elevated experience for your pet.",
            Location = "First floor",
            Price = 14
        },
        new()
        {
            Id = 7,
            HotelId = 3,
            RoomTypeId = 7,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
        },
        new()
        {
            Id = 8,
            HotelId = 4,
            RoomTypeId = 8,
            Name = "Luxury Lounge",
            Description = "An upgraded room featuring a larger, plush bed, premium bedding, and an array of toys and treats. Perfect for pets who enjoy a bit more luxury.",
            Location = "First floor",
            Price = 12
        },
    ];

}
