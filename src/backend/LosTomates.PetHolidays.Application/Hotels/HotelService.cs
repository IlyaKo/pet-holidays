using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Hotels;

public sealed class HotelService : IHotelService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper _mapper;

    public HotelService(ApplicationDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<HotelView>> GetAll()
        => _mapper.Map<List<HotelView>>(await dbContext.Hotels.Where(x => x.IsActive).ToListAsync());

    public async Task<HotelView?> GetById(int entityId)
    {
        var entity = await FindEntityById(entityId)
                 ?? throw new NotFoundException("hotel", entityId.ToString());

        return _mapper.Map<HotelView>(entity);
    }

    public async Task<int> Create(HotelEditDto dto)
    {
        var entity = new Hotel
        {
            Name = dto.Name,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, HotelEditDto dto)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException("hotel", entityId.ToString());

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsActive = dto.IsActive;

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
