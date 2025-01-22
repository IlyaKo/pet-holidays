using LosTomates.PetHolidays.Application.Pets;
using LosTomates.PetHolidays.Application.Rooms;

namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingView
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus BookingStatus { get; set; }

    public PetView Pet { get; set; }
  
    public RoomView Room { get; set; }
}