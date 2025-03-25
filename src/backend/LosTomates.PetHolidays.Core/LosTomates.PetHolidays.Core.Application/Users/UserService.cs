using FluentValidation;
using LosTomates.PetHolidays.Core.Core.Domain.Users;
using LosTomates.PetHolidays.Core.Core.Exceptions;
using LosTomates.PetHolidays.Core.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LosTomates.PetHolidays.Core.Application.Users;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IValidator<UserEditDto> validateService;
    private readonly ICurrentUserProvider _userProvider; 
    private readonly string _secretKey;

    public UserService(ApplicationDbContext dbContext, 
        IValidator<UserEditDto> validateService,
        ICurrentUserProvider userProvider,
        IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.validateService = validateService;
        _userProvider = userProvider;
        var configuredKey = configuration["JwtSettings:SecretKey"];
        if (string.IsNullOrEmpty(configuredKey))
            throw new ApplicationException("You need to set up the JWT secret key");
        _secretKey = configuredKey;
    }

    public async Task<UserView> GetById(string entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        return entity.Adapt<UserView>();
    }

    public async Task Create(string userId, UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);

        var user = dto.Adapt<User>();
        user.Id = userId;
        user.Name = dto.UserName;
        await dbContext.Users.AddAsync(user);
        dbContext.SaveChanges();
    }

    public async Task Update(string entityId, UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        dto.Adapt(entity);

        await dbContext.SaveChangesAsync();
    }

    public (string UserId, string UserName) GetUserFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == "nameid").Value;
            var userName = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

            return (userId, userName);
        }
        catch
        {
            throw new SecurityTokenException("Invalid token");
        }
    }

    public async Task<(string UserId, string UserName)> CurrentUser(ClaimsPrincipal userClaims)
    {
        if (userClaims == null || !userClaims.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var userId = userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userName = userClaims.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
        {
            throw new UnauthorizedAccessException("User information is missing.");
        }

        return (userId, userName);
    }

    private async Task<User?> FindEntityById(string entityId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == entityId);
    }

    public async Task<UserView> GetCurrentUser()
    {
        var userId = _userProvider.GetUserId();

        return await GetById(userId);
    }
}
