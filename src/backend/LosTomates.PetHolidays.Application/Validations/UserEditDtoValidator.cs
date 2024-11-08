using FluentValidation;
using FluentValidation.Results;
using LosTomates.PetHolidays.Application.Users;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Validations;

public class UserEditDtoValidator: AbstractValidator<UserEditDto>
{
    public UserEditDtoValidator() { }
}
