using FluentValidation;
using LosTomates.PetHolidays.Auth.Core.Domain.Users;
using LosTomates.PetHolidays.Auth.Core.Exceptions;
using LosTomates.PetHolidays.Auth.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LosTomates.PetHolidays.Auth.Application.Users;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IValidator<UserEditDto> validateService;
    private readonly UserManager<User> _userManager;
    private readonly UserClient userClient;
    private readonly ICurrentUserProvider _userProvider;
    private readonly string _secretKey;

    public UserService(ApplicationDbContext dbContext, 
        IValidator<UserEditDto> validateService,
        UserManager<User> userManager,
        UserClient userClient,
        ICurrentUserProvider userProvider,
        IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.validateService = validateService;
        this._userManager = userManager;
        this.userClient = userClient;
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

    public async Task<LoginResponse> Create(UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);

        _userManager.Options.Password.RequireNonAlphanumeric = false;
        _userManager.Options.User.RequireUniqueEmail = true;
        
        var user = dto.Adapt<User>();
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            throw new ValidationException(string.Join(", ", errors));
        }

        await userClient.CreateAsync(user.Id, user.UserName); 

        var loginDto = new LoginDto { Email = user.Email!, Password = dto.Password };
        return await Login(loginDto);
    }

    public async Task<LoginResponse> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email)
            ?? throw new UnauthorizedAccessException("Invalid credentials");

        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!result)
            throw new UnauthorizedAccessException("Invalid credentials");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName!)
            ]),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return new()
        {
            Token = token,
            Username = user.UserName!
        };
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
