using FluentValidation;
using LosTomates.PetHolidays.Core.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Core.Exceptions;
using LosTomates.PetHolidays.Core.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Core.Application.Hotels;

public sealed class HotelService(
    ApplicationDbContext dbContext,
    IValidator<HotelEditDto> validateService) : IHotelService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly IValidator<HotelEditDto> _validateService = validateService;

    public async Task<IReadOnlyList<HotelView>> GetAll()
    {
        return await _dbContext.Hotels
                              .Where(x => x.IsActive)
                              .ProjectToType<HotelView>()
                              .ToListAsync();
    }

    public async Task<HotelView> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

        return entity.Adapt<HotelView>();
    }

    public async Task<int> Create(HotelEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = dto.Adapt<Hotel>();

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, HotelEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

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

    private async Task<Hotel?> FindEntityById(int entityId)
    {
        return await _dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}