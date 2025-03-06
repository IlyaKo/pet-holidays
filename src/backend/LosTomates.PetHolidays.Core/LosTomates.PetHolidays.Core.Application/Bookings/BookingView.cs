using LosTomates.PetHolidays.Core.Application.Hotels;
using LosTomates.PetHolidays.Core.Application.Pets;
using LosTomates.PetHolidays.Core.Application.Rooms;
using LosTomates.PetHolidays.Core.Application.Users;

namespace LosTomates.PetHolidays.Core.Application.Bookings;

public class BookingView
{
    public int Id { get; set; }

    public required UserView User { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus Status { get; set; }

    public required PetView Pet { get; set; }

    public required HotelShortView Hotel { get; set; }

    public required RoomView Room { get; set; }
}