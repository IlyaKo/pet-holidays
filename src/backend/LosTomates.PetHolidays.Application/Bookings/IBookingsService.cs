namespace LosTomates.PetHolidays.Application.Bookings;

public interface IBookingsService
{
    Task<IReadOnlyList<BookingView>> GetAll();

    Task<BookingView> GetById(int hotelId);

    Task<int> Create(BookingsDto dto);

    Task Update(int entityId, BookingsDto dto);

    Task Delete(int entityId);
}
