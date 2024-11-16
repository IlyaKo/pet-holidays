using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Hotels;

public class HotelEditDtoValidator : AbstractValidator<HotelEditDto>
{
    public HotelEditDtoValidator()
    {
        RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}