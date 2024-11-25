using FluentValidation;
using LosTomates.PetHolidays.Core.Domain;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.PetTypes;

public sealed class PetTypeService : IPetTypeService
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IValidator<PetTypeEditDto> _validateService;

    public PetTypeService(ApplicationDbContext dbContext, IValidator<PetTypeEditDto> validateService)
    {
        _dbContext = dbContext;
        _validateService = validateService;
    }

    public async Task<IReadOnlyList<PetTypeView>> GetAll()
    {
        return await _dbContext.PetTypes
                              .Where(x => x.IsActive)
                              .ProjectToType<PetTypeView>()
                              .ToListAsync();
    }

    public async Task<PetTypeView?> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(PetType), entityId.ToString());

        return entity.Adapt<PetTypeView>();
    }

    public async Task<int> Create(PetTypeEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = dto.Adapt<PetType>();
  
        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task Update(int entityId, PetTypeEditDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(PetType), entityId.ToString());

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int entityId)
    {
        PetType? entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<PetType?> FindEntityById(int entityId)
    {
        return await _dbContext.PetTypes.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}

