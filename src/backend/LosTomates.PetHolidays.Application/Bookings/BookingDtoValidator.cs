using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingDtoValidator : AbstractValidator<BookingDto>
{
    public BookingDtoValidator()
    {
        // RuleFor(x => x.Name).Length(2, DatabaseConstrains.NameMaxLength);
        // RuleFor(x => x.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}