using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelService : IHotelService
{
    private readonly ApplicationDbContext dbContext;

    public HotelService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<HotelView>> GetAll()
        => await dbContext.Hotels
                  .Where(x => x.IsActive)
                  .ProjectToType<HotelView>()
                  .ToListAsync();

    public async Task<HotelView?> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                 ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

        return entity.Adapt<HotelView>();
    }

    public async Task<int> Create(HotelEditDto dto)
    {
        var entity = dto.Adapt<Hotel>();

        dbContext.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, HotelEditDto dto)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(Hotel), entityId.ToString());

        dto.Adapt(entity);

        await dbContext.SaveChangesAsync();
    }
    public async Task Delete(int entityId)
    {
        var entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    private async Task<Hotel?> FindEntityById(int entityId)
        => await dbContext.Hotels.FirstOrDefaultAsync(x => x.Id == entityId);
}
