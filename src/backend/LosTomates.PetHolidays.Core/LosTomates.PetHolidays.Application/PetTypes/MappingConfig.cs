using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using Mapster;

namespace LosTomates.PetHolidays.Core.Application.PetTypes;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PetType, PetTypeView>();

        config.NewConfig<PetTypeEditDto, PetType>();

    }
}
