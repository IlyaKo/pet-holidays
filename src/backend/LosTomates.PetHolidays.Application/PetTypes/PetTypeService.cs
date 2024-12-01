using FluentValidation;
using LosTomates.PetHolidays.Application.Shared;
using LosTomates.PetHolidays.Core.Domain;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.PetTypes;

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