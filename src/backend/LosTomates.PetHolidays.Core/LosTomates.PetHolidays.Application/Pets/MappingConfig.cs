using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using Mapster;

namespace LosTomates.PetHolidays.Core.Application.Pets;
public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Pet, PetView>();

        config.NewConfig<PetEditDto, Pet>();

    }
}
