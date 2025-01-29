using FluentValidation;
using LosTomates.PetHolidays.Core.DataAccess;

namespace LosTomates.PetHolidays.Core.Application.PetTypes;
public class PetTypeEditDtoValidator : AbstractValidator<PetTypeEditDto>
{
    public PetTypeEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
    }
}
