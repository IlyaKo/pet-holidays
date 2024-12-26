namespace LosTomates.PetHolidays.Core.Domain.Bookings;

public class Booking : BaseEntity
{
    public string UserId { get; set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus BookingStatus { get; set; }
}