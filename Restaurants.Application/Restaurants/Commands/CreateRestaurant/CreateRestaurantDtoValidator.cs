using FluentValidation;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Vietnamese", "Italian", "Japanese"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(6, 100);
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required");
        RuleFor(dto => dto.Category)
            .NotEmpty().WithMessage("Category is required");
        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide valid mail");
        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code");
        RuleFor(dto => dto.Category).Custom((value, context) =>
        {
            var isValidCategory = validCategories.Contains(value);
            if (!isValidCategory)
            {
                context.AddFailure("Please choose valid category");
            }
        });
    }
}