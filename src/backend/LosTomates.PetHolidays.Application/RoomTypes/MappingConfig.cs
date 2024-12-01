using LosTomates.PetHolidays.Core.Domain.Rooms;
using Mapster;

namespace LosTomates.PetHolidays.Application.RoomTypes;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RoomType, RoomTypeView>();

        config.NewConfig<RoomTypeEditDto, RoomType>();
    }
}

