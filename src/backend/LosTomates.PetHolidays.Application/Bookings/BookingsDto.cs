namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingsDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus BookingStatus { get; set; }
}