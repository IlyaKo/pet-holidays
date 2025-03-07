using FluentValidation;
using LosTomates.PetHolidays.Core.Application.PetTypes;
using LosTomates.PetHolidays.Core.Application.Users;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Core.Exceptions;
using LosTomates.PetHolidays.Core.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Core.Application.Pets;

public sealed class PetService(
    ApplicationDbContext dbContext,
    IValidator<PetEditDto> validator,
    IPetTypeService petTypeService,
    IUserService userService) : IPetService
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IValidator<PetEditDto> _validator = validator;
    private readonly IPetTypeService _petTypeService = petTypeService;
    private readonly IUserService _userService = userService;
    public async Task<int> Create(string userId, PetEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        await _userService.GetById(userId);
        await _petTypeService.GetById(dto.PetTypeId);

        var pet = dto.Adapt<Pet>();
        pet.PetOwnerId = userId;

        _dbContext.Add(pet);

        await _dbContext.SaveChangesAsync();

        return pet.Id;
    }

    public async Task Delete(int petId)
    {
        var entity = await FindEntityById(petId);

        if (entity is null)
            return;

        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PetView> GetById(int petId)
    {
        var pet = await FindEntityById(petId)
                       ?? throw new NotFoundException(nameof(Pet), $"id: {petId}");

        return pet.Adapt<PetView>();
    }

    public async Task<IReadOnlyList<PetView>> GetByUserId(string userId)
          => await _dbContext.Pets.Where(x => x.PetOwnerId == userId)
                                  .ProjectToType<PetView>()
                                  .ToListAsync();

    public async Task Update(int petId, PetEditDto dto)
    {
        _validator.ValidateAndThrow(dto);

        await _petTypeService.GetById(dto.PetTypeId);

        var entity = await FindEntityById(petId)
                  ?? throw new NotFoundException(nameof(Pet), $"id: {petId}");

        dto.Adapt(entity);

        await _dbContext.SaveChangesAsync();
    }

    private async Task<Pet?> FindEntityById(int petId)
       => await _dbContext.Pets.Include(p=>p.PetType)
                               .FirstOrDefaultAsync(x => x.Id == petId);

}