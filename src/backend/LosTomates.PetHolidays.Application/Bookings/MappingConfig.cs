using LosTomates.PetHolidays.Core.Domain.Bookings;
using Mapster;

namespace LosTomates.PetHolidays.Application.Bookings;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Booking, BookingView>();

        config.NewConfig<BookingsDto, Booking>();
    }
}