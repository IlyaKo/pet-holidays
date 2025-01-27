using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Domain.Users;

namespace LosTomates.PetHolidays.Core.Domain.Bookings;

public class Booking : BaseEntity
{
    public required string UserId { get; set; }

    public int HotelId { get;set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public User? User { get; set; }

    public Hotel? Hotel { get; set; }

    public Room? Room { get; set; }

    public Pet? Pet { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus Status { get; set; }
}