using FluentValidation;
using LosTomates.PetHolidays.Core.DataAccess;

namespace LosTomates.PetHolidays.Core.Application.RoomTypes;

public class RoomTypeEditDtoValidator : AbstractValidator<RoomTypeEditDto>
{
    public RoomTypeEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}