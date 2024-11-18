
using FluentValidation;
using FluentValidation.Results;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.DataAccess;

namespace LosTomates.PetHolidays.Application.Validations;
public class HotelEditDtoValidator : AbstractValidator<HotelEditDto>
{
    public HotelEditDtoValidator()
    {
        RuleFor(customer => customer.Name).Length(2, DatabaseConstrains.NameMaxLength);
        RuleFor(customer => customer.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength);
    }
}