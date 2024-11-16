using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Rooms;

public sealed class RoomService(
    ApplicationDbContext dbContext,
    IValidator<RoomEditDto> validator,
    IHotelService hotelService,
    IRoomTypeService roomTypeService) : IRoomService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IValidator<RoomEditDto> _validator = validator;
    private readonly IHotelService _hotelService = hotelService;
    private readonly IRoomTypeService _roomTypeService = roomTypeService;

    public async Task<IReadOnlyList<RoomView>> GetAll(int hotelId)
        => await _dbContext.Rooms
                           .Where(x => x.HotelId == hotelId)
                           .ProjectToType<RoomView>()
                           .ToListAsync();

    public async Task<RoomView?> GetById(int hotelId, int entityId)
    {
        var entity = await FindEntityById(hotelId, entityId)
                  ?? throw new NotFoundException(nameof(Room), $"id: {entityId} and hotel id: {hotelId}");

        return entity.Adapt<RoomView>();
    }

    public async Task<int> Create(int hotelId, RoomEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        // check related entities exist
        await _hotelService.GetById(hotelId);
        await _roomTypeService.GetById(hotelId, dto.RoomTypeId);

        var entity = dto.Adapt<Room>();
        entity.HotelId = hotelId;

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int hotelId, int entityId, RoomEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        // check related entities exist
        await _roomTypeService.GetById(hotelId, dto.RoomTypeId);

        var entity = await FindEntityById(hotelId, entityId)
                  ?? throw new NotFoundException(nameof(Room), $"id: {entityId} and hotel id: {hotelId}");

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int hotelId, int entityId)
    {
        var entity = await FindEntityById(hotelId, entityId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Room?> FindEntityById(int hotelId, int entityId)
        => await _dbContext.Rooms
                           .FirstOrDefaultAsync(x => x.HotelId == hotelId
                                                  && x.Id == entityId);
}
