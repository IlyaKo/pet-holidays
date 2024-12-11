using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using MockQueryable.Moq;
using Moq;

namespace LosTomates.PetHolidays.Tests;

public class RoomServiceTests
{
    private readonly Mock<ApplicationDbContext> _dbContextMock;

    private readonly Mock<IValidator<RoomEditDto>> _validatorMock;

    private readonly Mock<IHotelService> _hotelServiceMock;

    private readonly Mock<IRoomTypeService> _roomTypeServiceMock;

    private readonly IRoomService _roomService;

    public RoomServiceTests()
    {
        _dbContextMock = new Mock<ApplicationDbContext>();
        _validatorMock = new Mock<IValidator<RoomEditDto>>();
        _hotelServiceMock = new Mock<IHotelService>();
        _roomTypeServiceMock = new Mock<IRoomTypeService>();

        _roomService = new RoomService(_dbContextMock.Object,
                                       _validatorMock.Object,
                                       _hotelServiceMock.Object,
                                       _roomTypeServiceMock.Object);
    }

    [Fact]
    public void GetAll_Should_Return_All_Rooms_Of_Hotel()
    {
        // Arrange

        var hotel = 0;
        var hotelId = 0;

        // Act
        //var result = _roomService.GetAll(hotelId);

        // Assert
        // Assert.True();
    }

    [Fact]
    public void GetById_Should_Return_Room_With_Requested_Id()
    {
        // async Task<RoomView?> GetById(int hotelId, int entityId)

        // Arrange

        // Act

        // Assert
        // Assert.True();
    }

    [Fact]
    public void Create_Should_Create_Room()
    {
        // async Task<int> Create(int hotelId, RoomEditDto dto)

        // Arrange

        // Act
        // var hotelId = 0;
        // var roomEditDto = new RoomEditDto { Name = "" };
        // var result =  _roomService.Create(hotelId, roomEditDto);

        // Assert
        // Assert.True();
    }

    [Fact]
    public void Update_Should_Update_Room()
    {
        // async Task Update(int hotelId, int entityId, RoomEditDto dto)

        // Arrange

        // Act
        // var hotelId = 0;
        // var entityId = 0;
        // var roomEditDto = new RoomEditDto { Name = "" };
        // var result =  _roomService.Update(hotelId, entityId, roomEditDto);

        // Assert
        // Assert.True();
    }

    [Fact]
    public async void Delete_Should_Delete_Room()
    {
        // async Task Delete(int hotelId, int entityId)

        // Arrange

        // Act
        // var hotelId = 0;
        // var entityId = 0;
        // await _roomService.Delete(hotelId, entityId);

        // Assert
        // Assert.True();
    }



    [Fact]
    public async Task GetAll_Returns_Rooms_For_Hotel()
    {
        // // Arrange
        // var hotelId = 1;
        // var rooms = new List<Room>
        // {
        //     new Room { Id = 1, HotelId = hotelId, Name = "Room 1" },
        //     new Room { Id = 2, HotelId = hotelId, Name = "Room 2" }
        // };

        // var dbSetMock = rooms.AsQueryable().BuildMockDbSet();
        // _dbContextMock.Setup(x => x.Rooms)
        //               .Returns(dbSetMock.Object);

        // // Act
        // var result = await _roomService.GetAll(hotelId);

        // // Assert
        // Assert.Equal(2, result.Count);
        // Assert.Contains(result, r => r.Name == "Room 1");
        // Assert.Contains(result, r => r.Name == "Room 2");
    }

    [Fact]
    public async Task GetById_Throws_NotFoundException_When_Room_Does_Not_Exist()
    {
        // // Arrange
        // var hotelId = 1;
        // var roomId = 99;

        // var dbSetMock = new List<Room>().AsQueryable()
        //                                 .BuildMockDbSet();
        // _dbContextMock.Setup(x => x.Rooms)
        //               .Returns(dbSetMock.Object);

        // // Act & Assert
        // await Assert.ThrowsAsync<NotFoundException>(() => _roomService.GetById(hotelId, roomId));
    }

    [Fact]
    public async Task Create_Validates_Input_And_Saves_Room()
    {
        // // Arrange
        // var hotelId = 1;
        // var dto = new RoomEditDto { Name = "New Room", RoomTypeId = 2 };

        // _validatorMock.Setup(v => v.ValidateAndThrow(dto));
        // _hotelServiceMock.Setup(h => h.GetById(hotelId))
        //                  .Returns(Task.CompletedTask);
        // _roomTypeServiceMock.Setup(r => r.GetById(dto.RoomTypeId))
        //                     .Returns(Task.CompletedTask);

        // var dbSetMock = new List<Room>().AsQueryable()
        //                                 .BuildMockDbSet();
        // _dbContextMock.Setup(x => x.Rooms)
        //               .Returns(dbSetMock.Object);

        // // Act
        // var result = await _roomService.Create(hotelId, dto);

        // // Assert
        // _dbContextMock.Verify(x => x.Add(It.Is<Room>(r => r.Name == dto.Name && r.HotelId == hotelId)));
        // _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Update_Throws_NotFoundException_When_Room_Does_Not_Exist()
    {
        // Arrange
        var hotelId = 1;
        var roomId = 99;
        var roomEditDto = new RoomEditDto { Name = "Updated Room", RoomTypeId = 2 };

        _validatorMock.Setup(v => v.ValidateAndThrow(roomEditDto));
        _roomTypeServiceMock.Setup(r => r.GetById(roomEditDto.RoomTypeId))
                            .Returns(new Task<RoomTypeView>(() => new RoomTypeView
                            { 
                                Id = 0,
                                Name = "",
                                Description = ""
                            }));

        var dbSetMock = new List<Room>().AsQueryable()
                                        .BuildMockDbSet();
        _dbContextMock.Setup(x => x.Rooms)
                      .Returns(dbSetMock.Object);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _roomService.Update(hotelId, roomId, roomEditDto));
    }

    [Fact]
    public async Task Delete_Removes_Room_When_Exists()
    {
        // Arrange
        var hotelId = 1;
        var roomId = 1;
        var room = new Room 
        {
            Name = "Test room", 
            Id = roomId, 
            HotelId = hotelId 
        };

        var dbSetMock = new List<Room> { room }.AsQueryable()
                                               .BuildMockDbSet();
        _dbContextMock.Setup(x => x.Rooms)
                      .Returns(dbSetMock.Object);

        // Act
        await _roomService.Delete(hotelId, roomId);

        // Assert
        _dbContextMock.Verify(x => x.Remove(It.Is<Room>(r => r.Id == roomId && r.HotelId == hotelId)));
        _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
    }
}