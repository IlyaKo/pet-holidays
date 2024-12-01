using LosTomates.PetHolidays.Core.Domain;
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
