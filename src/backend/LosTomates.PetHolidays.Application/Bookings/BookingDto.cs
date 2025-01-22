namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingDto
{
    public required string UserId { get; set; }

    public required int RoomId { get; set; }

    public required int PetId { get; set; }

    public required DateTime CheckInDate { get; set; }

    public required DateTime CheckOutDate { get; set; }
}