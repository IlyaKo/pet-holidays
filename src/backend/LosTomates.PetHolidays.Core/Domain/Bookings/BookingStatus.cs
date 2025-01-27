namespace LosTomates.PetHolidays.Core.Domain.Bookings;

public enum BookingStatus : int
{
    None,
    Created,
    CheckIn,
    Cancelled,
    Removed
}