namespace LosTomates.PetHolidays.Application.Bookings;

public interface IBookingsService
{
    Task<IReadOnlyList<BookingView>> GetAll();

    Task<BookingView> GetById(int hotelId);

    Task<int> Create(BookingDto dto);

    Task Update(int entityId, BookingDto dto);

    Task Delete(int entityId);
}
