using LosTomates.PetHolidays.Core.Domain.Pets;
using Mapster;

namespace LosTomates.PetHolidays.Application.PetTypes;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PetType, PetTypeView>();

        config.NewConfig<PetTypeEditDto, PetType>();

    }
}
