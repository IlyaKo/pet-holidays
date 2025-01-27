namespace LosTomates.PetHolidays.Application.Bookings;

public interface IBookingService
{
    Task<BookingView> GetById(int hotelId);

    Task<IReadOnlyList<BookingView>> GetByUserId(string userId);

    Task<int> Create(BookingDto dto);

    Task Update(int entityId, BookingDto dto);

    Task UpdateStatus(int entityId, UpdateBookingDto dto);

    Task Delete(int entityId);
}
