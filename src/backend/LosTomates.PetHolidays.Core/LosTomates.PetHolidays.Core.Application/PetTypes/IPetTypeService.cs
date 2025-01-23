using LosTomates.PetHolidays.Core.Core.Abstractions;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;

namespace LosTomates.PetHolidays.Core.Application.PetTypes;

public interface IPetTypeService : ICrudService<PetType, PetTypeView, PetTypeEditDto>
{
}

