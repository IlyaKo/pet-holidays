using LosTomates.PetHolidays.Core.Domain.Pets;
using Mapster;

namespace LosTomates.PetHolidays.Application.Pets;
public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Pet, PetView>()
             .Map(dest => dest.PetType, src => src.PetType != null ? src.PetType.Name : "Unknown");

        config.NewConfig<PetEditDto, Pet>();

    }
}
