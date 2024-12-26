namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingDto
{
    public string UserId { get; set; }

    public int RoomId { get; set; }

    public int PetId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }
}