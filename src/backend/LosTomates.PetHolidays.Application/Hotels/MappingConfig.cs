using LosTomates.PetHolidays.Core.Domain.Hotels;
using Mapster;

namespace LosTomates.PetHolidays.Application.Hotels;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Hotel, HotelView>();

        config.NewConfig<HotelEditDto, Hotel>();

    }
}

