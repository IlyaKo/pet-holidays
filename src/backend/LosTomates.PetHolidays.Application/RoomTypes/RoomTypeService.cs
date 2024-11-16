using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.RoomTypes;

public sealed class RoomTypeService(
    ApplicationDbContext dbContext,
    IValidator<RoomTypeEditDto> validator,
    IHotelService hotelService) : IRoomTypeService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IValidator<RoomTypeEditDto> _validator = validator;
    private readonly IHotelService _hotelService = hotelService;

    public async Task<IReadOnlyList<RoomTypeView>> GetAll(int hotelId)
        => await _dbContext.RoomTypes
                           .Where(x => x.HotelId == hotelId)
                           .ProjectToType<RoomTypeView>()
                           .ToListAsync();

    public async Task<RoomTypeView> GetById(int hotelId, int entityId)
    {
        var entity = await FindEntityById(hotelId, entityId)
                  ?? throw new NotFoundException(nameof(RoomType), $"id: {entityId} and hotel id: {hotelId}");

        return entity.Adapt<RoomTypeView>();
    }

    public async Task<int> Create(int hotelId, RoomTypeEditDto dto)
    {
        // check the hotel exists
        await _hotelService.GetById(hotelId);
        _validator.ValidateAndThrow(dto);

        var entity = dto.Adapt<RoomType>();
        entity.HotelId = hotelId;

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int hotelId, int entityId, RoomTypeEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        var entity = await FindEntityById(hotelId, entityId)
                  ?? throw new NotFoundException(nameof(RoomType), $"id: {entityId} and hotel id: {hotelId}");

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

    private async Task<RoomType?> FindEntityById(int hotelId, int entityId)
        => await _dbContext.RoomTypes
                           .FirstOrDefaultAsync(x => x.HotelId == hotelId
                                                  && x.Id == entityId);
}
