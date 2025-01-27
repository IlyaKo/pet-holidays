using FluentValidation;
using LosTomates.PetHolidays.Core.Domain.Bookings;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Bookings;

public sealed class BookingService(ApplicationDbContext dbContext, IValidator<BookingDto> validateService) : IBookingService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly IValidator<BookingDto> _validateService = validateService;

    public async Task<BookingView> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

        return entity.Adapt<BookingView>();
    }

    public async Task<IReadOnlyList<BookingView>> GetByUserId(string userId)
    {
        var userBookings = await _dbContext.Bookings.Where(x => x.UserId == userId)
                                                    .Include(x => x.UserId)
                                                    .Include(x => x.PetId)
                                                    .Include(x => x.HotelId)
                                                    .Include(x => x.RoomId)
                                                    .ToListAsync();

        return userBookings.Adapt<IReadOnlyList<BookingView>>();
    }

    public async Task<int> Create(BookingDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = dto.Adapt<Booking>();
        entity.Status = Core.Domain.Bookings.BookingStatus.Created;

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, BookingDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Booking), entityId.ToString());

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateStatus(int entityId, UpdateBookingDto dto)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Booking), entityId.ToString());
        
        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int entityId)
    {
        var entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    private async Task<Booking?> FindEntityById(int entityId)
    {
        return await _dbContext.Bookings.Include(x => x.UserId)
                                        .Include(x => x.PetId)
                                        .Include(x => x.HotelId)
                                        .Include(x => x.RoomId)
                                        .FirstOrDefaultAsync(x => x.Id == entityId);
    }
}