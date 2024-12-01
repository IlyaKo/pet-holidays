using LosTomates.PetHolidays.Core.Abstractions;
using LosTomates.PetHolidays.Core.Domain;

namespace LosTomates.PetHolidays.Application.PetTypes;

public interface IPetTypeService : ICrudService<PetType, PetTypeView, PetTypeEditDto>
{
}

