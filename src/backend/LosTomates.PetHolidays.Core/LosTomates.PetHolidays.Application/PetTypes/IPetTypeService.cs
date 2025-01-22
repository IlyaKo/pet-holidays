using LosTomates.PetHolidays.Core.Abstractions;
using LosTomates.PetHolidays.Core.Domain.Pets;

namespace LosTomates.PetHolidays.Application.PetTypes;

public interface IPetTypeService : ICrudService<PetType, PetTypeView, PetTypeEditDto>
{
}

