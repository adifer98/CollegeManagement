using FluentValidation;

namespace CollegeManagement.Application.Ratings.Commands.CreateRating;

public class CreateRatingCommandValidator : AbstractValidator<CreateRatingCommand>
{
    public CreateRatingCommandValidator()
    {
        RuleFor(x => x.Rate)
            .LessThanOrEqualTo(5)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Rates are whole and between 1 and 5");
    }
}