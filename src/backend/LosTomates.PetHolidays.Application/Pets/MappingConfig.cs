using LosTomates.PetHolidays.Core.Domain.Pets;
using Mapster;

namespace LosTomates.PetHolidays.Application.Pets;
public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Pet, PetView>();

        config.NewConfig<PetEditDto, Pet>();

    }
}
