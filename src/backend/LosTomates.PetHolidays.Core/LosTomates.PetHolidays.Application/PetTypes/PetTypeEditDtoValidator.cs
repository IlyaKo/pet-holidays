using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.PetTypes;
public class PetTypeEditDtoValidator : AbstractValidator<PetTypeEditDto>
{
    public PetTypeEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
    }
}
