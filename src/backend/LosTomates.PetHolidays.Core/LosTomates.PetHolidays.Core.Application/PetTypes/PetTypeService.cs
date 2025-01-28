using FluentValidation;
using LosTomates.PetHolidays.Core.Application.Shared;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.DataAccess;

namespace LosTomates.PetHolidays.Core.Application.PetTypes;

public sealed class PetTypeService : CrudService<PetType, PetTypeView, PetTypeEditDto>, IPetTypeService
{
    public PetTypeService(ApplicationDbContext dbContext, IValidator<PetTypeEditDto> validateService)
        : base(dbContext, validateService)
    {
    }

    public async override Task<IReadOnlyList<PetTypeView>> GetAll()
    {
        return await GetAll(x => x.IsActive);
    }
}