using FluentValidation;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingDtoValidator : AbstractValidator<BookingDto>
{
    public BookingDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty()
                              .Length(1, DatabaseConstrains.UserIdMaxLength);

        RuleFor(x => x.RoomId).GreaterThan(0);

        RuleFor(x => x.PetId).GreaterThan(0);

        RuleFor(x => x.CheckInDate).GreaterThanOrEqualTo(DateTime.Today);

        RuleFor(x => x.CheckOutDate).GreaterThan(x => x.CheckInDate);
    }
}