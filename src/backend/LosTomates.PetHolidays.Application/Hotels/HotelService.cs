using FluentValidation;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelService : IHotelService
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IValidator<HotelEditDto> _validateService;

    public HotelService(ApplicationDbContext dbContext, IValidator<HotelEditDto> validateService)
    {
        _dbContext = dbContext;
        _validateService = validateService;
    }

    public async Task<IReadOnlyList<HotelView>> GetAll()
    {
        return await _dbContext.Hotels
                               .Where(x => x.IsActive)
                               .Select(x => new HotelView(x))
                               .ToListAsync();
    }

    public async Task<HotelView?> GetById(int entityId)
    {
        bool isValid = entityId >= 0; 
        if(!isValid)
            throw new ValidationBadRequest($"Недопустимый идентификатор {entityId}");

        return await _dbContext.Hotels
                               .Where(x => x.Id == entityId)
                               .Select(x => new HotelView(x))
                               .FirstOrDefaultAsync();
    }

    public async Task<int> Create(HotelEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        Hotel entity = new Hotel
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, HotelEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        Hotel entity = await FindEntityById(entityId) ?? throw new NotFoundException("hotel", entityId.ToString());


        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int entityId)
    {
        bool isValid = entityId >= 0; 
        if(!isValid)
            throw new ValidationBadRequest($"Недопустимый идентификатор {entityId}");

        Hotel? entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Hotel?> FindEntityById(int entityId)
    {     
        bool isValid = entityId >= 0; 
        if(!isValid)
            throw new ValidationBadRequest($"Недопустимый идентификатор {entityId}");

        return await _dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}