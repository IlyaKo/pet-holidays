using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Bookings;

public class HotelEditDtoValidator : AbstractValidator<BookingsDto>
{
    public HotelEditDtoValidator()
    {
        // RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        // RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}