using LosTomates.PetHolidays.Core.Core.Domain.Bookings;
using Mapster;

namespace LosTomates.PetHolidays.Core.Application.Bookings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Booking, BookingView>();

        config.NewConfig<BookingDto, Booking>();
    }
}