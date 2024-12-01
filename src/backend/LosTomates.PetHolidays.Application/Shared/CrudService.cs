using FluentValidation;
using LosTomates.PetHolidays.Core.Abstractions;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LosTomates.PetHolidays.Application.Shared;

public class CrudService<TEntity, TView, TEditDto> : ICrudService<TEntity, TView, TEditDto>
        where TEntity : class
        where TView : class
        where TEditDto : class
{
    protected readonly ApplicationDbContext _dbContext;
    private readonly IValidator<TEditDto>? _validateService;

    public CrudService(ApplicationDbContext dbContext, IValidator<TEditDto>? validateService = null)
    {
        _dbContext = dbContext;
        _validateService = validateService;
    }

    public virtual async Task<IReadOnlyList<TView>> GetAll()
    {
        return await _dbContext.Set<TEntity>()
            .ProjectToType<TView>()
            .ToListAsync();
    }
    public virtual async Task<IReadOnlyList<TView>> GetAll(Expression<Func<TEntity, bool>>? where = null)
    {
        var query = _dbContext.Set<TEntity>().AsQueryable();

        if (where != null)
            query = query.Where(where);

        return await query
            .ProjectToType<TView>()
            .ToListAsync();
    }

    public virtual async Task<TView> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
            ?? throw new NotFoundException(typeof(TEntity).Name, entityId.ToString());

        return entity.Adapt<TView>();
    }

    public virtual async Task<TView> Create(TEditDto dto)
    {
        _validateService?.ValidateAndThrow(dto);

        var entity = dto.Adapt<TEntity>();

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Adapt<TView>();
    }

    public virtual async Task<TView> Update(int entityId, TEditDto dto)
    {
        _validateService?.ValidateAndThrow(dto);

        var entity = await FindEntityById(entityId)
            ?? throw new NotFoundException(typeof(TEntity).Name, entityId.ToString());

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Adapt<TView>();
    }

    public virtual async Task Delete(int entityId)
    {
        var entity = await FindEntityById(entityId);

        if (entity == null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<TEntity?> FindEntityById(int entityId)
    {
        return await _dbContext.Set<TEntity>().FindAsync(entityId);
    }


}
