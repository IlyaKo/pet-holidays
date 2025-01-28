namespace LosTomates.PetHolidays.Core.Application.Bookings;

public enum BookingStatus : int
{
    None,
    Created,
    CheckIn,
    Cancelled,
    Removed
}