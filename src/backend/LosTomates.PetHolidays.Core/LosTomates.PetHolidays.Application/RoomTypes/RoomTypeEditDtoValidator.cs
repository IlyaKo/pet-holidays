using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.RoomTypes;

public class RoomTypeEditDtoValidator : AbstractValidator<RoomTypeEditDto>
{
    public RoomTypeEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}