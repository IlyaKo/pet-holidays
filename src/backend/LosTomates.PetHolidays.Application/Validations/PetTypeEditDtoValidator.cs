using FluentValidation;
using LosTomates.PetHolidays.Application.PetTypes;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Validations;
public class PetTypeEditDtoValidator : AbstractValidator<PetTypeEditDto>
{
    public PetTypeEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
    }
}
