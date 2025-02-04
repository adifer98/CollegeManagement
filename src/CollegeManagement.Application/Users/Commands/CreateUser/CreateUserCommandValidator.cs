
using FluentValidation;

namespace CollegeManagement.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3)
            .MaximumLength(100).WithMessage("Name must be between 3 and 100 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
    }
}