using FluentAssertions;
using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.DataAccess;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LosTomates.PetHolidays.Tests;

public class RoomServiceTests
{
    private readonly ApplicationDbContext _dbContext;

    private readonly Mock<IValidator<RoomEditDto>> _validatorMock;

    private readonly Mock<IHotelService> _hotelServiceMock;

    private readonly Mock<IRoomTypeService> _roomTypeServiceMock;

    private readonly IRoomService _roomService;

    public RoomServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;
        _dbContext = new ApplicationDbContext(options);

        _validatorMock = new Mock<IValidator<RoomEditDto>>();
        _hotelServiceMock = new Mock<IHotelService>();
        _roomTypeServiceMock = new Mock<IRoomTypeService>();

        _roomService = new RoomService(_dbContext, 
                                       _validatorMock.Object,
                                       _hotelServiceMock.Object,
                                       _roomTypeServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_Should_Return_All_Rooms_Of_Hotel() 
    {
        // Arrange
        var hotel = SeedHotel();
        var room = SeedRoom(hotel.Id);
        var roomsCountInHotel = _dbContext.Rooms.Where(r => r.HotelId == hotel.Id).Count();

        // Act
        var result = await _roomService.GetAll(hotel.Id);

        // Assert
        result.Count.Should().Be(roomsCountInHotel);
    }

    [Fact]
    public async Task GetById_Should_Return_Room_With_Requested_Id()
    {
        // Arrange
        var hotel = SeedHotel();
        var room = SeedRoom(hotel.Id);

        // Act
        var result = await _roomService.GetById(hotel.Id, room.Id);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Create_Should_Create_Room() 
    {
        // Arrange
        var hotel = SeedHotel();
        var room = SeedRoom(hotel.Id);

        var roomEditDto = new RoomEditDto
        {
            Name = "New test room",
        };

        // Act
        var result = await _roomService.Create(hotel.Id, roomEditDto);

        // Assert
        var newRoom = _dbContext.Rooms.FirstOrDefault(r => r.Id == result);
        newRoom.Should().NotBeNull();
    }

    [Fact]
    public async Task Update_Should_Update_Room()
    {
        // Arrange
        var hotel = SeedHotel();
        var room = SeedRoom(hotel.Id);

        var newName = "Updated Room";
        var newRoomTypeId = 2;
        var roomEditDto = new RoomEditDto { Name = newName, RoomTypeId = newRoomTypeId };

        // Act
        await _roomService.Update(hotel.Id, room.Id, roomEditDto);

        // Assert
        var result = _dbContext.Rooms.FirstOrDefault(r => r.HotelId == hotel.Id && r.Id == room.Id);
        result.Should().NotBeNull();
        result.RoomTypeId.Should().Be(newRoomTypeId, because: $"Ожидаем RoomTypeId = {newRoomTypeId}");
        result.Name.Should().Be(newName, because: $"Ожидаем Name = {newName}");
    }

    [Fact]
    public async Task Delete_Removes_Room_When_Exists()
    {
        // Arrange
        var hotel = SeedHotel();
        var room = SeedRoom(hotel.Id);

        // Act
        await _roomService.Delete(hotel.Id, room.Id);

        // Assert
        var result = _dbContext.Rooms.FirstOrDefault(r => r.HotelId == hotel.Id && r.Id == room.Id);
        result.Should().BeNull();      
    }

    private Hotel SeedHotel()
    {
        var hotelEntity = _dbContext.Hotels.Add(new Hotel
        {
            Name = "Test hotel",
        });

        _dbContext.SaveChanges();
        return hotelEntity.Entity;
    }

    private Room SeedRoom(int hotelId)
    {
        var roomEntity = _dbContext.Rooms.Add(new Room
        {
            HotelId = hotelId,
            Name = "Test room",
            RoomTypeId = 1
        });

        _dbContext.SaveChanges();
        return roomEntity.Entity;
    }
}