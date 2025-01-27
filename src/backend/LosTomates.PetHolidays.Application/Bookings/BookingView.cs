using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Pets;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.Users;

namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingView
{
    public int Id { get; set; }

    public required UserView User { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus Status { get; set; }

    public required PetView Pet { get; set; }
  
    public required HotelView Hotel { get; set; }

    public required RoomView Room { get; set; }
}