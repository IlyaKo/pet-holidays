using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Rooms;

public class RoomEditDtoValidator : AbstractValidator<RoomEditDto>
{
    public RoomEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
        RuleFor(x => x.Location).MaximumLength(DatabaseConstrains.AddressMaxLength);
        RuleFor(x => x.RoomTypeId).GreaterThan(0);
        RuleFor(x => x.Price).GreaterThan(0);
    }
}