namespace LosTomates.PetHolidays.WebApi;
using FluentValidation;
using FluentValidation.Results;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.DataAccess;

public class HotelEditDtoValidator : AbstractValidator<HotelEditDto>
{
    public HotelEditDtoValidator()
    {
        string message = "Ошибка в поле {PropertyName}: значение {PropertyValue}";

        RuleFor(customer => customer.Name).NotNull()
                                          .MaximumLength(DatabaseConstrains.NameMaxLength)
                                          .WithMessage(message);
        RuleFor(customer => customer.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength)
                                                 .When(v => !string.IsNullOrEmpty(v.Description))
                                                 .WithMessage(message);
    }

    public override ValidationResult Validate(ValidationContext<HotelEditDto> context)
    {
        ValidationResult result = base.Validate(context);
        
        foreach(ValidationFailure? error in result.Errors)
            Console.WriteLine(error.ErrorMessage);

        return result;
    }
}