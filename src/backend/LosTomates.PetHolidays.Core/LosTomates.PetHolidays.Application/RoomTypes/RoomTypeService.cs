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

    public async Task<IReadOnlyList<RoomTypeView>> GetAll()
        => await _dbContext.RoomTypes
                           .ProjectToType<RoomTypeView>()
                           .ToListAsync();

    public async Task<RoomTypeView> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(RoomType), entityId.ToString());

        return entity.Adapt<RoomTypeView>();
    }

    public async Task<int> Create(RoomTypeEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        var entity = dto.Adapt<RoomType>();

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, RoomTypeEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(RoomType), entityId.ToString());

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int entityId)
    {
        var entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<RoomType?> FindEntityById(int entityId)
        => await _dbContext.RoomTypes
                           .FirstOrDefaultAsync(x => x.Id == entityId);
}
