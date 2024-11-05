namespace LosTomates.PetHolidays.Application;
using FluentValidation;
using FluentValidation.Results;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.DataAccess;

public class HotelEditDtoValidator : AbstractValidator<HotelEditDto>
{
    public HotelEditDtoValidator()
    {
        RuleFor(customer => customer.Name).MaximumLength(DatabaseConstrains.NameMaxLength);  
        RuleFor(customer => customer.Description).MaximumLength(DatabaseConstrains.DescriptionMaxLength); 
    }

    public override ValidationResult Validate(ValidationContext<HotelEditDto> context)
    {
        ValidationResult result = base.Validate(context);
        
        foreach(ValidationFailure? error in result.Errors)
            Console.WriteLine(error.ErrorMessage);

        return result;
    }
}