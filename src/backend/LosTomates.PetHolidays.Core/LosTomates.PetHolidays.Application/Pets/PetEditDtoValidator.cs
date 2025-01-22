using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Pets;
public class PetEditDtoValidator : AbstractValidator<PetEditDto>
{
    public PetEditDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(DatabaseConstrains.NameMaxLength);
    }
}
