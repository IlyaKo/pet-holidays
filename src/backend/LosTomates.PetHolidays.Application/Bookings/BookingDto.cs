namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingDto
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public BookingStatus BookingStatus { get; set; }
}