namespace LosTomates.PetHolidays.Core.Domain.Hotels;

public sealed class Reservation
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public Hotel? Hotel { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
