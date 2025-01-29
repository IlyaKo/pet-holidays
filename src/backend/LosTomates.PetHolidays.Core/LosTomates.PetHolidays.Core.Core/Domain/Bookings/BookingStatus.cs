namespace LosTomates.PetHolidays.Core.Core.Domain.Bookings;

public enum BookingStatus : int
{
    None,
    Created,
    CheckIn,
    Cancelled,
    Removed
}