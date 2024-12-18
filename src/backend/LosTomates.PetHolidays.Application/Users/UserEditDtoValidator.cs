using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Users;

public class UserEditDtoValidator : AbstractValidator<UserEditDto>
{
    public UserEditDtoValidator()
    {
        RuleFor(x => x.UserName).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(x => x.PhoneNumber).Length(2, DatabaseConstrains.PhoneNumberMaxLength);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}
