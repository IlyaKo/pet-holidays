using FluentValidation;
using LosTomates.PetHolidays.Application.Pets;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Validations;
public class PetEditDtoValidator : AbstractValidator<PetEditDto>
{
    public PetEditDtoValidator()
    { }
}
