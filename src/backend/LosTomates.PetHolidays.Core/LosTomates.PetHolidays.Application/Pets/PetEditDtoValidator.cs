using FluentValidation;
using LosTomates.PetHolidays.Core.DataAccess;

namespace LosTomates.PetHolidays.Core.Application.Pets;
public class PetEditDtoValidator : AbstractValidator<PetEditDto>
{
    public PetEditDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(DatabaseConstrains.NameMaxLength);
    }
}
