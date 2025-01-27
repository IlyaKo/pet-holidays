namespace LosTomates.PetHolidays.Application.Bookings;

public enum BookingStatus : int
{
    None,
    Created,
    CheckIn,
    Cancelled,
    Removed
}