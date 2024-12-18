namespace LosTomates.PetHolidays.Application.Bookings;

public enum BookingStatus : int
{
    /// <summary>
    /// Not set
    /// </summary>
    None,
    /// <summary>
    /// Booking created
    /// </summary>
    Created,
    /// <summary>
    /// In use
    /// </summary>
    CheckIn,
    /// <summary>
    /// Booking cancelled
    /// </summary>
    Cancelled,
    /// <summary>
    /// Reservation archived
    /// </summary>
    Removed
}