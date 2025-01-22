using LosTomates.PetHolidays.Core.Domain.Rooms;
using Mapster;

namespace LosTomates.PetHolidays.Application.Rooms;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Room, RoomView>();

        config.NewConfig<RoomEditDto, Room>();
    }
}

