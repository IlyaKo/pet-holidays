using System.Linq.Expressions;

namespace LosTomates.PetHolidays.Core.Abstractions;

public interface ICrudService<TEntity, TView, TEditDto>
    where TEntity : class
    where TView : class
    where TEditDto : class
{
    Task<IReadOnlyList<TView>> GetAll();
    Task<IReadOnlyList<TView>> GetAll(Expression<Func<TEntity, bool>>? where = null);
    Task<TView?> GetById(int entityId);
    Task<TView> Create(TEditDto dto);
    Task<TView> Update(int entityId, TEditDto dto);
    Task Delete(int entityId);
}
