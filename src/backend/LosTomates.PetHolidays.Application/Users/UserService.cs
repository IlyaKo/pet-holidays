using FluentValidation;
using LosTomates.PetHolidays.Core.Domain.Users;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LosTomates.PetHolidays.Application.Users;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;
    private readonly IValidator<UserEditDto> validateService;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly string _secretKey;

    public UserService(ApplicationDbContext dbContext, IValidator<UserEditDto> validateService,
                       UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.dbContext = dbContext;
        this.validateService = validateService;
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._secretKey = "H3ll0W0rld!Th1s1s4T3mp0raryS3cr3tK3yF0rT3sting0nly!";
    }

    public async Task<UserView> GetById(string entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        return entity.Adapt<UserView>();
    }

    public async Task<string> Create(UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);
        var user = dto.Adapt<User>();
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            return user.Id;
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        throw new ValidationException(string.Join(", ", errors));
    }

    public async Task Update(string entityId, UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        dto.Adapt(entity);

        await dbContext.SaveChangesAsync();
    }

    public async Task<string> Login(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), dto.Email);
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<(string UserId, string UserName)>  GetUserFromToken(string token)
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



    private async Task<User?> FindEntityById(string entityId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}
