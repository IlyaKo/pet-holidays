using FluentAssertions;
using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public async void GetAll_Should_Return_All_Rooms_Of_Hotel() 
    {
        // Arrange
        int hotelId, roomId;
        EntityEntry<Hotel> hotelEntity;
        EntityEntry<Room> roomEntity;
        SeedHotelAndRoom(out hotelId, out roomId, out hotelEntity, out roomEntity);
        var roomsCountInHotel = _dbContext.Rooms.Where(r => r.HotelId == hotelId).Count();

        // Act
        var result = await _roomService.GetAll(hotelId);

        // Assert
        result.Count.Should().Be(roomsCountInHotel);
    }

    [Fact]
    public async void GetById_Should_Return_Room_With_Requested_Id()
    {
        // Arrange
        int hotelId, roomId;
        EntityEntry<Hotel> hotelEntity;
        EntityEntry<Room> roomEntity;
        SeedHotelAndRoom(out hotelId, out roomId, out hotelEntity, out roomEntity);

        // Act
        var result = await _roomService.GetById(hotelId, roomId);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async void Create_Should_Create_Room() 
    {
        // Arrange
        int hotelId, roomId;
        EntityEntry<Hotel> hotelEntity;
        EntityEntry<Room> roomEntity;
        SeedHotelAndRoom(out hotelId, out roomId, out hotelEntity, out roomEntity);

        var roomEditDto = new RoomEditDto
        {
            Name = "New test room",
        };

        // Act
        var result = await _roomService.Create(hotelId, roomEditDto);

        // Assert
        var newRoom = _dbContext.Rooms.FirstOrDefault(r => r.Id == result);
        newRoom.Should().NotBeNull();
    }

    [Fact]
    public async Task Update_Should_Update_Room()
    {
        // Arrange
        int hotelId, roomId;
        EntityEntry<Hotel> hotelEntity;
        EntityEntry<Room> roomEntity;
        SeedHotelAndRoom(out hotelId, out roomId, out hotelEntity, out roomEntity);

        var newName = "Updated Room";
        var newRoomTypeId = 2;
        var roomEditDto = new RoomEditDto { Name = newName, RoomTypeId = newRoomTypeId };

        // Act
        await _roomService.Update(hotelEntity.Entity.Id, roomEntity.Entity.Id, roomEditDto);

        // Assert
        var result = _dbContext.Rooms.FirstOrDefault(r => r.HotelId == hotelId && r.Id == roomId);
        result.Should().NotBeNull();
        result.RoomTypeId.Should().Be(newRoomTypeId, because: $"Ожидаем RoomTypeId = {newRoomTypeId}");
        result.Name.Should().Be(newName, because: $"Ожидаем Name = {newName}");
    }

    [Fact]
    public async Task Delete_Removes_Room_When_Exists()
    {
        // Arrange
        int hotelId, roomId;
        EntityEntry<Hotel> hotelEntity;
        EntityEntry<Room> roomEntity;
        SeedHotelAndRoom(out hotelId, out roomId, out hotelEntity, out roomEntity);

        // Act
        await _roomService.Delete(hotelEntity.Entity.Id, roomEntity.Entity.Id);

        // Assert
        var result = _dbContext.Rooms.FirstOrDefault(r => r.HotelId == hotelId && r.Id == roomId);
        result.Should().BeNull();      
    }

    private void SeedHotelAndRoom(out int hotelId, out int roomId, out EntityEntry<Hotel> hotelEntity, out EntityEntry<Room> roomEntity)
    {
        hotelId = 11;
        roomId = 22;

        hotelEntity = _dbContext.Hotels.Add(new Hotel
        {
            Id = hotelId,
            Name = "Test hotel",
        });
        roomEntity = _dbContext.Rooms.Add(new Room
        {
            Id = roomId,
            HotelId = hotelId,
            Name = "Test room",
            RoomTypeId = 1
        });

        _dbContext.SaveChanges();
    }
}