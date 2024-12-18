using FluentValidation;
using LosTomates.PetHolidays.Core.Domain.Bookings;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Bookings;

public sealed class BookingsService(ApplicationDbContext dbContext, IValidator<BookingsDto> validateService) : IBookingsService
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    private readonly IValidator<BookingsDto> _validateService = validateService;

    public async Task<IReadOnlyList<BookingView>> GetAll()
    {
        return await _dbContext.Bookings
                               .Where(x => x.BookingStatus != Core.Domain.Bookings.BookingStatus.None 
                                        && x.BookingStatus != Core.Domain.Bookings.BookingStatus.Removed
                                        && x.BookingStatus != Core.Domain.Bookings.BookingStatus.Cancelled)
                               .ProjectToType<BookingView>()
                               .ToListAsync();
    }

    public async Task<BookingView> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

        return entity.Adapt<BookingView>();
    }

    public async Task<int> Create(BookingsDto dto)
    {
        _validateService.ValidateAndThrow(dto);

        var entity = dto.Adapt<Booking>();

        _dbContext.Add(entity);

        await _dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, BookingsDto dto)
    {
        _validateService.ValidateAndThrow(dto);

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
        return await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}